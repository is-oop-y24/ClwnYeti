using System;
using System.Collections.Generic;
using Banks.Interfaces;

namespace Banks.Classes
{
    public class DebitAccount : IAccount
    {
        private readonly Guid _id;
        private readonly Guid _idOfOwner;
        private readonly List<decimal> _bufferOfInterest;
        public DebitAccount(Guid idOfOwner)
        {
            Balance = 0;
            _idOfOwner = idOfOwner;
            _bufferOfInterest = new List<decimal>();
            _id = Guid.NewGuid();
        }

        public DebitAccount()
        {
            Balance = 0;
            _idOfOwner = Guid.Empty;
            _bufferOfInterest = new List<decimal>();
            _id = Guid.Empty;
        }

        public decimal Balance { get; private set; }

        public void ChargeInterests(int months)
        {
            for (int i = 0; i < months * 30; i++)
            {
                Balance += _bufferOfInterest[i];
            }

            _bufferOfInterest.RemoveRange(0, months * 30);
        }

        public decimal CheckCommission(BankConfiguration bankConfiguration)
        {
            return 0;
        }

        public void SkipDays(int days, BankConfiguration bankConfiguration)
        {
            for (int i = 0; i < days; i++)
            {
                _bufferOfInterest.Add(Balance * (bankConfiguration.DebitAccountConfiguration.Interest / 365));
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
            return $"Debit Account with id {_id}: Owner Id - {_idOfOwner}, Balance - {Balance}";
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
            return Balance >= money;
        }
    }
}