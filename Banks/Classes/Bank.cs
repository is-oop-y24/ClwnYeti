using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Classes
{
    public class Bank : IBank
    {
    private readonly List<IAccount> _accounts;
    private readonly Dictionary<Guid, ClientInfo> _clientsInfo;
    private readonly Dictionary<Guid, List<Transaction>> _transactions;

    public Bank(string name, BankConfiguration bankConfiguration)
    {
        Name = name;
        Configuration = bankConfiguration;
        _transactions = new Dictionary<Guid, List<Transaction>>();
        _accounts = new List<IAccount>();
        _clientsInfo = new Dictionary<Guid, ClientInfo>();
        Id = Guid.NewGuid();
    }

    public BankConfiguration Configuration { get; }
    public string Name { get; }
    public Guid Id { get; }

    public void SkipTime(TimeToSkip skippedTime)
    {
        foreach (IAccount account in _accounts)
        {
            account.ChargeInterests(skippedTime.Months);
            account.SkipDays(skippedTime.Days, Configuration);
        }
    }

    public Client AddClient(Client client)
    {
        _clientsInfo[client.Id] = new ClientInfo(client);
        return client;
    }

    public void UpdateClient(Guid clientId, Client client)
    {
        if (!_clientsInfo.ContainsKey(clientId)) throw new BankException("No such client in this bank");
        _clientsInfo[clientId].Client = client;
    }

    public IAccount AddDebitAccountForClient(Guid clientId)
    {
        if (!_clientsInfo.ContainsKey(clientId)) throw new BankException("No such client in this bank");
        if (!_clientsInfo[clientId].TypesOfAccounts.Any(a => a.EqualsWith(new DebitAccount())))
            _clientsInfo[clientId].TypesOfAccounts.Add(new DebitAccount());
        _accounts.Add(new DebitAccount(clientId));
        _transactions[_accounts[^1].GetId()] = new List<Transaction>();
        return _accounts[^1];
    }

    public IAccount AddDepositAccountForClient(Guid clientId, int remainingDaysForWork)
    {
        if (!_clientsInfo.ContainsKey(clientId)) throw new BankException("No such client in this bank");
        if (!_clientsInfo[clientId].TypesOfAccounts.Any(a => a.EqualsWith(new DepositAccount())))
            _clientsInfo[clientId].TypesOfAccounts.Add(new DepositAccount());
        _accounts.Add(new DepositAccount(clientId, remainingDaysForWork));
        _transactions[_accounts[^1].GetId()] = new List<Transaction>();
        return _accounts[^1];
    }

    public IAccount AddCreditAccountForClient(Guid clientId)
    {
        if (!_clientsInfo.ContainsKey(clientId)) throw new BankException("No such client in this bank");
        if (!_clientsInfo[clientId].TypesOfAccounts.Any(a => a.EqualsWith(new CreditAccount())))
            _clientsInfo[clientId].TypesOfAccounts.Add(new DepositAccount());
        _accounts.Add(new CreditAccount(clientId));
        _transactions[_accounts[^1].GetId()] = new List<Transaction>();
        return _accounts[^1];
    }

    public List<Transaction> TransferBetweenAccounts(Guid accountIdFrom, decimal money, Guid accountIdTo)
    {
        IAccount accountFrom = GetAccount(accountIdFrom);
        IAccount accountTo = GetAccount(accountIdTo);
        if (money > Configuration.CriticalAmountOfMoney && !GetClient(accountFrom.GetOwnerId()).IsVerified())
            throw new BankException("Owner of accounts is not verified");
        if (money > Configuration.CriticalAmountOfMoney && !GetClient(accountTo.GetOwnerId()).IsVerified())
            throw new BankException("Owner of accounts is not verified");
        decimal commissionFrom = accountFrom.CheckCommission(Configuration);
        decimal commissionTo = accountTo.CheckCommission(Configuration);
        accountFrom.Withdraw(money + commissionFrom, Configuration);
        accountTo.Replenish(money - commissionTo, Configuration);
        var id = Guid.NewGuid();
        var transactions = new List<Transaction>();
        _transactions[accountIdFrom].Add(new Transaction(id, Id, Id, accountIdFrom, money + commissionFrom, accountIdTo));
        transactions.Add(_transactions[accountIdFrom][^1]);
        _transactions[accountIdTo].Add(new Transaction(id, Id, Id, accountIdFrom, money - commissionTo, accountIdTo));
        transactions.Add(_transactions[accountIdTo][^1]);
        return transactions;
    }

    public Transaction ReplenishToAccount(decimal money, Guid accountIdTo)
    {
        IAccount account = GetAccount(accountIdTo);
        if (money > Configuration.CriticalAmountOfMoney && !GetClient(account.GetOwnerId()).IsVerified())
            throw new BankException("Owner of accounts is not verified");
        if (!account.CanMakeReplenish(money, Configuration)) throw new BankException("Can't make a transaction");
        decimal commission = account.CheckCommission(Configuration);
        account.Replenish(money - commission, Configuration);
        _transactions[accountIdTo].Add(new Transaction(Guid.NewGuid(), Guid.Empty, Id, Guid.Empty, money - commission, accountIdTo));
        return _transactions[accountIdTo][^1];
    }

    public Transaction ReplenishToAccount(Guid id, Guid accountFromBankId, Guid accountFromId, decimal money, Guid accountIdTo)
    {
        IAccount account = GetAccount(accountIdTo);
        if (money > Configuration.CriticalAmountOfMoney && !GetClient(account.GetOwnerId()).IsVerified())
            throw new BankException("Owner of accounts is not verified");
        decimal commission = account.CheckCommission(Configuration);
        account.Replenish(money - commission, Configuration);
        _transactions[accountIdTo].Add(new Transaction(id, accountFromBankId, Id, accountFromId, money - commission, accountIdTo));
        return _transactions[accountIdTo][^1];
    }

    public Transaction WithdrawFromAccount(Guid accountIdFrom, decimal money)
    {
        IAccount account = GetAccount(accountIdFrom);
        if (money > Configuration.CriticalAmountOfMoney && !GetClient(account.GetOwnerId()).IsVerified())
            throw new BankException("Owner of accounts is not verified");
        if (!account.CanMakeWithdraw(money, Configuration)) throw new BankException("Can't make a transaction");
        decimal commission = account.CheckCommission(Configuration);
        account.Withdraw(money + commission, Configuration);
        _transactions[accountIdFrom].Add(new Transaction(Guid.NewGuid(), Id, Guid.Empty, accountIdFrom, money + commission, Guid.Empty));
        return _transactions[accountIdFrom][^1];
    }

    public Transaction WithdrawFromAccount(Guid id, Guid accountToBankId, Guid accountToId, Guid accountIdFrom, decimal money)
    {
        IAccount account = GetAccount(accountIdFrom);
        if (money > Configuration.CriticalAmountOfMoney && !GetClient(account.GetOwnerId()).IsVerified())
            throw new BankException("Owner of accounts is not verified");
        decimal commission = account.CheckCommission(Configuration);
        account.Withdraw(money + commission, Configuration);
        _transactions[accountIdFrom].Add(new Transaction(id, Id, accountToBankId, accountIdFrom, money + commission, accountToId));
        return _transactions[accountIdFrom][^1];
    }

    public Transaction GetLastTransactionOfAccount(Guid accountId)
    {
        if (!_transactions.ContainsKey(accountId)) throw new BankException("Such account doesn't exist in this bank");
        if (_transactions[accountId].Count == 0) throw new BankException("Account didn't had any transactions");
        return _transactions[accountId][^1];
    }

    public void CancelLastTransaction(Guid accountId)
    {
        IAccount account = GetAccount(accountId);
        if (_transactions[accountId].Count == 0) throw new BankException("Account didn't had any transactions");
        Transaction transaction = _transactions[accountId][^1];
        if (account.GetId() == transaction.AccountIdTo)
        {
            account.Withdraw(transaction.Money, Configuration);
        }
        else
        {
            account.Replenish(transaction.Money, Configuration);
        }

        _transactions[accountId].RemoveAt(_transactions[accountId].Count - 1);
    }

    public IEnumerable<string> Notify(IAccount account)
    {
        return _clientsInfo.Values.Where(ci => ci.TypesOfAccounts.Any(a => a.EqualsWith(account)))
            .Select(clientInfo => clientInfo.Client.Notify()).ToList();
    }

    public string GetInfo()
    {
        return $"Bank with id {Id}: Name - {Name}";
    }

    public Client GetClient(Guid clientId)
    {
        foreach (ClientInfo ci in _clientsInfo.Values.Where(ci => ci.Client.Id == clientId))
        {
            return ci.Client;
        }

        throw new BankException("There is no client with this id");
    }

    public IAccount GetAccount(Guid accountId)
    {
        foreach (IAccount account in _accounts.Where(account => account.GetId() == accountId))
        {
            return account;
        }

        throw new BankException("There is no account with this id");
    }
    }
}