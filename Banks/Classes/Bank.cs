using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Classes
{
    public class Bank
    {
        private readonly List<IAccount> _accounts;
        private readonly List<Client> _clients;
        private readonly Dictionary<Guid, List<Transaction>> _transactions;

        public Bank(string name, BankConfiguration bankConfiguration)
        {
            Name = name;
            Configuration = bankConfiguration;
            _transactions = new Dictionary<Guid, List<Transaction>>();
            _accounts = new List<IAccount>();
            _clients = new List<Client>();
            Id = Guid.NewGuid();
        }

        public BankConfiguration Configuration { get; set; }
        public string Name { get; }
        public Guid Id { get; }

        public void SkipDays(int days)
        {
            foreach (IAccount account in _accounts)
            {
                account.ChargeInterests(days, Configuration);
                account.CheckCommission(days, Configuration);
            }
        }

        public Client AddClient(Client client)
        {
            _clients.Add(client);
            return _clients[^1];
        }

        public IAccount AddDebitAccountForClient(Guid clientId)
        {
            _accounts.Add(new DebitAccount(clientId));
            _transactions[_accounts[^1].GetId()] = new List<Transaction>();
            return _accounts[^1];
        }

        public IAccount AddDepositAccountForClient(Guid clientId)
        {
            if (_clients.All(c => c.Id != clientId)) throw new BankException("No such client in this bank");
            _accounts.Add(new DepositAccount(clientId));
            _transactions[_accounts[^1].GetId()] = new List<Transaction>();
            return _accounts[^1];
        }

        public IAccount AddCreditAccountForClient(Guid clientId)
        {
            if (_clients.All(c => c.Id != clientId)) throw new BankException("No such client in this bank");
            _accounts.Add(new CreditAccount(clientId));
            _transactions[_accounts[^1].GetId()] = new List<Transaction>();
            return _accounts[^1];
        }

        public void MakeTransferBetweenAccounts(Guid accountIdFrom, decimal money, Guid accountIdTo)
        {
            foreach (IAccount a in _accounts.Where(a => a.GetId() == accountIdFrom))
            {
                if (money > Configuration.CriticalAmountOfMoney && !IfClientVerified(a.GetOwnerId()))
                    throw new BankException("Owner of one of accounts is not verified");
                foreach (IAccount b in _accounts.Where(b => b.GetId() == accountIdTo))
                {
                    if (money > Configuration.CriticalAmountOfMoney && !IfClientVerified(a.GetOwnerId()))
                        throw new BankException("Owner of one of accounts is not verified");
                    a.Withdraw(money, Configuration);
                    b.Replenish(money, Configuration);
                    _transactions[accountIdFrom].Add(new Transaction(accountIdFrom, money, accountIdTo));
                    _transactions[accountIdTo].Add(new Transaction(accountIdFrom, money, accountIdTo));
                }
            }
        }

        public void ReplenishAccount(decimal money, Guid accountIdTo)
        {
            foreach (IAccount a in _accounts.Where(a => a.GetId() == accountIdTo))
            {
                if (money > Configuration.CriticalAmountOfMoney && !IfClientVerified(a.GetOwnerId()))
                    throw new BankException("Owner of accounts is not verified");
                a.Replenish(money, Configuration);
                _transactions[accountIdTo].Add(new Transaction(money, accountIdTo, Guid.NewGuid()));
            }
        }

        public void WithdrawFromAccount(Guid accountIdFrom, decimal money)
        {
            foreach (IAccount a in _accounts.Where(a => a.GetId() == accountIdFrom))
            {
                if (money > Configuration.CriticalAmountOfMoney && !IfClientVerified(a.GetOwnerId()))
                    throw new BankException("Owner of accounts is not verified");
                a.Withdraw(money, Configuration);
                _transactions[accountIdFrom].Add(new Transaction(accountIdFrom, money, Guid.NewGuid()));
            }
        }

        public void CancelLastTransaction(Guid accountId)
        {
            if (!_transactions.ContainsKey(accountId))
                throw new BankException("Such account doesn't exist in this bank");
            if (_transactions[accountId].Count == 0) throw new BankException("Account didn't had any transactions");
            if (_transactions[accountId][^1].AccountIdFrom == accountId)
            {
                if (_transactions[accountId][^1].AccountIdTo == Guid.Empty)
                {
                    foreach (IAccount a in _accounts.Where(a => a.GetId() == accountId))
                    {
                        a.Replenish(_transactions[accountId][^1].Money, Configuration);
                        _transactions[accountId].RemoveAt(_transactions[accountId].Count);
                    }
                }
                else
                {
                    if (!Equals(_transactions[_transactions[accountId][^1].AccountIdTo][^1], _transactions[accountId][^1]))
                    {
                        throw new BankException("Can't cancel this transaction");
                    }

                    foreach (IAccount a in _accounts.Where(a => a.GetId() == _transactions[accountId][^1].AccountIdTo))
                    {
                        a.Withdraw(_transactions[accountId][^1].Money, Configuration);
                        _transactions[_transactions[accountId][^1].AccountIdTo].RemoveAt(_transactions[_transactions[accountId][^1].AccountIdTo].Count);
                    }

                    foreach (IAccount a in _accounts.Where(a => a.GetId() == accountId))
                    {
                        a.Replenish(_transactions[accountId][^1].Money, Configuration);
                        _transactions[accountId].RemoveAt(_transactions[accountId].Count);
                    }
                }
            }

            if (_transactions[accountId][^1].AccountIdFrom == Guid.Empty)
            {
                foreach (IAccount a in _accounts.Where(a => a.GetId() == accountId))
                {
                    a.Withdraw(_transactions[accountId][^1].Money, Configuration);
                }
            }
            else
            {
                if (!Equals(_transactions[_transactions[accountId][^1].AccountIdFrom][^1], _transactions[accountId][^1]))
                {
                    throw new BankException("Can't cancel this transaction");
                }

                foreach (IAccount a in _accounts.Where(a => a.GetId() == _transactions[accountId][^1].AccountIdFrom))
                {
                    a.Replenish(_transactions[accountId][^1].Money, Configuration);
                    _transactions[_transactions[accountId][^1].AccountIdFrom].RemoveAt(_transactions[_transactions[accountId][^1].AccountIdFrom].Count);
                }

                foreach (IAccount a in _accounts.Where(a => a.GetId() == accountId))
                {
                    a.Withdraw(_transactions[accountId][^1].Money, Configuration);
                    _transactions[accountId].RemoveAt(_transactions[accountId].Count);
                }
            }
        }

        private bool IfClientVerified(Guid clientId)
        {
            foreach (Client c in _clients.Where(c => c.Id == clientId))
            {
                return c.IsVerified();
            }

            throw new BankException("No such client in this bank");
        }
    }
}