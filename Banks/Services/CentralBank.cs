using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Classes;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Services
{
    public class CentralBank : ICentralBank
    {
        private readonly List<Bank> _banks;
        private readonly TimeMachine _timeMachine;

        public CentralBank()
        {
            _banks = new List<Bank>();
            _timeMachine = new TimeMachine();
        }

        public void SkipTime(TimeSpan timeToSkip)
        {
            TimeToSkip skippedTime = _timeMachine.HowMuchToSkip(timeToSkip);
            foreach (Bank bank in _banks)
            {
                bank.SkipTime(skippedTime);
            }
        }

        public Bank AddBank(string name)
        {
            _banks.Add(new Bank(name, BankConfiguration.Default()));
            return _banks[^1];
        }

        public Bank AddBank(string name, BankConfiguration bankConfiguration)
        {
            _banks.Add(new Bank(name, bankConfiguration));
            return _banks[^1];
        }

        public Bank GetBank(Guid bankId)
        {
            foreach (Bank b in _banks.Where(b => b.Id == bankId))
            {
                return b;
            }

            throw new BankException("There is no bank with this id");
        }

        public Transaction TransferBetweenAccountsFromDifferentBanks(Guid accountFromBankId, Guid accountFromId, Guid accountToBankId, Guid accountToId, decimal money)
        {
            Bank accountFromBank = GetBank(accountFromBankId);
            Bank accountToBank = GetBank(accountFromBankId);
            IAccount accountFrom = accountFromBank.GetAccount(accountFromId);
            IAccount accountTo = accountToBank.GetAccount(accountToId);
            if (!accountFrom.CanMakeWithdraw(money, accountFromBank.Configuration) ||
                !accountTo.CanMakeReplenish(money, accountToBank.Configuration))
                throw new BankException("Can't make a transaction");
            var id = Guid.NewGuid();
            accountFromBank.WithdrawFromAccount(id, accountToBankId, accountToId, accountFromId, money);
            return accountFromBank.ReplenishToAccount(id, accountFromBankId, accountFromId, money, accountToId);
        }

        public void CancelLastTransactionOfAccountInBank(Guid accountBankId, Guid accountId)
        {
            Bank bankOfAccount = GetBank(accountBankId);
            IAccount account = bankOfAccount.GetAccount(accountId);
            Transaction transaction = bankOfAccount.GetLastTransactionOfAccount(accountId);
            IAccount secondAccount;
            if (transaction.AccountFromBankId == transaction.AccountToBankId)
            {
                if (transaction.AccountIdTo == accountId)
                {
                    secondAccount = bankOfAccount.GetAccount(transaction.AccountIdFrom);
                    if (!account.CanMakeWithdraw(transaction.Money, bankOfAccount.Configuration) || !secondAccount.CanMakeReplenish(transaction.Money, bankOfAccount.Configuration)) throw new BankException("Can't cancel this transaction");
                }
                else
                {
                    secondAccount = bankOfAccount.GetAccount(transaction.AccountIdTo);
                    if (!account.CanMakeReplenish(transaction.Money, bankOfAccount.Configuration) || !secondAccount.CanMakeWithdraw(transaction.Money, bankOfAccount.Configuration)) throw new BankException("Can't cancel this transaction");
                }

                bankOfAccount.CancelLastTransaction(accountId);
                bankOfAccount.CancelLastTransaction(transaction.AccountIdTo);
                return;
            }

            if (transaction.AccountIdFrom == accountId)
            {
                if (transaction.AccountToBankId == Guid.Empty)
                {
                    if (!account.CanMakeReplenish(transaction.Money, bankOfAccount.Configuration)) throw new BankException("Can't cancel this transaction");
                    bankOfAccount.CancelLastTransaction(accountId);
                    return;
                }

                Bank bankOfSecondAccount = GetBank(transaction.AccountToBankId);
                if (!transaction.Equals(bankOfSecondAccount.GetLastTransactionOfAccount(transaction.AccountIdTo))) throw new BankException("Can't cancel this transaction");
                secondAccount = bankOfSecondAccount.GetAccount(transaction.AccountIdTo);
                if (!account.CanMakeReplenish(transaction.Money, bankOfAccount.Configuration) || !secondAccount.CanMakeWithdraw(transaction.Money, bankOfAccount.Configuration)) throw new BankException("Can't cancel this transaction");
                bankOfAccount.CancelLastTransaction(accountId);
                bankOfAccount.CancelLastTransaction(transaction.AccountIdTo);
            }
            else
            {
                if (transaction.AccountFromBankId == Guid.Empty)
                {
                    if (!account.CanMakeWithdraw(transaction.Money, bankOfAccount.Configuration)) throw new BankException("Can't cancel this transaction");
                    bankOfAccount.CancelLastTransaction(accountId);
                    return;
                }

                Bank bankOfSecondAccount = GetBank(transaction.AccountFromBankId);
                if (!transaction.Equals(bankOfSecondAccount.GetLastTransactionOfAccount(transaction.AccountIdFrom))) throw new BankException("Can't cancel this transaction");
                secondAccount = bankOfSecondAccount.GetAccount(transaction.AccountIdFrom);
                if (!account.CanMakeWithdraw(transaction.Money, bankOfAccount.Configuration) || !secondAccount.CanMakeReplenish(transaction.Money, bankOfAccount.Configuration)) throw new BankException("Can't cancel this transaction");
                bankOfAccount.CancelLastTransaction(accountId);
                bankOfAccount.CancelLastTransaction(transaction.AccountIdFrom);
            }
        }
    }
}