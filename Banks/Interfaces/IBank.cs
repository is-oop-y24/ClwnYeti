using System;
using System.Collections.Generic;
using Banks.Classes;

namespace Banks.Interfaces
{
    public interface IBank
    {
        public void SkipDays(int days);
        public Client AddClient(Client client);

        public void UpdateClient(Guid clientId, Client client);
        public IAccount AddDebitAccountForClient(Guid clientId);
        public IAccount AddDepositAccountForClient(Guid clientId);
        public IAccount AddCreditAccountForClient(Guid clientId);
        public void MakeTransferBetweenAccounts(Guid accountIdFrom, decimal money, Guid accountIdTo);
        public void ReplenishToAccount(decimal money, Guid accountIdTo);
        public void WithdrawFromAccount(Guid accountIdFrom, decimal money);
        public void CancelLastTransaction(Guid accountId);
        public IEnumerable<string> Notify(IAccount account);

        public string GetInfo();

        public Client GetClient(Guid clientId);
    }
}