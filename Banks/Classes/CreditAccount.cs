using System;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Classes
{
    public class CreditAccount : IAccount
    {
        private readonly Guid _id;
        private readonly Guid _idOfOwner;

        public CreditAccount(Guid idOfOwner)
        {
            Balance = 0;
            _idOfOwner = idOfOwner;
            _id = Guid.NewGuid();
        }

        public CreditAccount()
        {
            Balance = 0;
            _idOfOwner = Guid.Empty;
            _id = Guid.Empty;
        }

        public decimal Balance { get; private set; }

        public void ChargeInterests(int month, BankConfiguration bankConfiguration)
        {
        }

        public void CheckCommission(int days, BankConfiguration bankConfiguration)
        {
            if (Balance < 0)
            {
                Balance -= days * bankConfiguration.CreditAccountConfiguration.Commission;
            }
        }

        public void Replenish(decimal money, BankConfiguration bankConfiguration)
        {
            Balance += money;
        }

        public void Withdraw(decimal money, BankConfiguration bankConfiguration)
        {
            Balance -= money;
        }

        public Guid GetId()
        {
            return _id;
        }

        public Guid GetOwnerId()
        {
            return _idOfOwner;
        }

        public string GetInfo()
        {
            return $"Credit Account with id {_id}: Owner Id - {_idOfOwner}, Balance - {Balance}";
        }

        public bool EqualsWith(IAccount account)
        {
            return GetType() == account.GetType() && _id == account.GetId() && _idOfOwner == GetOwnerId();
        }

        public bool CanMakeReplenish(decimal money, BankConfiguration bankConfiguration)
        {
            return true;
        }

        public bool CanMakeWithdraw(decimal money, BankConfiguration bankConfiguration)
        {
            return Balance - money >= -bankConfiguration.CreditAccountConfiguration.CreditLimit;
        }
    }
}