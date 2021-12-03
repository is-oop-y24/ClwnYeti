using System;
using System.Collections.Generic;
using Banks.Classes;

namespace Banks.Interfaces
{
    public interface IBank
    {
        public void SkipTime(TimeToSkip skippedTime);
        public Client AddClient(Client client);

        public void UpdateClient(Guid clientId, Client client);
        public IAccount AddDebitAccountForClient(Guid clientId);
        public IAccount AddDepositAccountForClient(Guid clientId, int remainingDaysForWork);
        public IAccount AddCreditAccountForClient(Guid clientId);
        public Transaction TransferBetweenAccounts(Guid accountIdFrom, decimal money, Guid accountIdTo);
        public Transaction ReplenishToAccount(decimal money, Guid accountIdTo);

        public Transaction ReplenishToAccount(Guid id, Guid accountFromBankId, Guid accountFromId, decimal money, Guid accountIdTo);
        public Transaction WithdrawFromAccount(Guid accountIdFrom, decimal money);

        public Transaction WithdrawFromAccount(Guid id, Guid accountToBankId, Guid accountToId, Guid accountIdFrom, decimal money);
        public Transaction GetLastTransactionOfAccount(Guid accountId);
        public void CancelLastTransaction(Guid accountId);
        public IEnumerable<string> Notify(IAccount account);
        public string GetInfo();
        public Client GetClient(Guid clientId);
        public IAccount GetAccount(Guid accountId);
    }
}