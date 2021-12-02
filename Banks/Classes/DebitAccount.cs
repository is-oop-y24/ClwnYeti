using System;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Classes
{
    public class DebitAccount : IAccount
    {
        private readonly Guid _id;
        private readonly Guid _idOfOwner;
        public DebitAccount(Guid idOfOwner)
        {
            Balance = 0;
            _idOfOwner = idOfOwner;
            _id = Guid.NewGuid();
        }

        public DebitAccount()
        {
            Balance = 0;
            _idOfOwner = Guid.Empty;
            _id = Guid.Empty;
        }

        public decimal Balance { get; private set; }

        public void ChargeInterests(int days, BankConfiguration bankConfiguration)
        {
            for (int i = 0; i < days; i++)
            {
                Balance *= 1 + (bankConfiguration.DebitAccountConfiguration.Interest / 100);
            }
        }

        public void CheckCommission(int days, BankConfiguration bankConfiguration)
        {
        }

        public void Replenish(decimal money, BankConfiguration bankConfiguration)
        {
            Balance += money;
        }

        public void Withdraw(decimal money, BankConfiguration bankConfiguration)
        {
            if (Balance - money < 0) throw new BankException("Not enough money on account");
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
            return $"Debit Account with id {_id}: Owner Id - {_idOfOwner}, Balance - {Balance}";
        }

        public bool EqualsWith(IAccount account)
        {
            return GetType() == account.GetType() && _id == account.GetId() && _idOfOwner == GetOwnerId();
        }
    }
}