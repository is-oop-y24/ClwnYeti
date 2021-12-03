using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Interfaces;

namespace Banks.Classes
{
    public class DepositAccount : IAccount
    {
        private readonly Guid _id;
        private readonly Guid _idOfOwner;
        private readonly List<decimal> _bufferOfInterest;
        public DepositAccount(Guid idOfOwner, int remainingDays)
        {
            _idOfOwner = idOfOwner;
            RemainingDays = remainingDays;
            _bufferOfInterest = new List<decimal>();
            Balance = 0;
            _id = Guid.NewGuid();
        }

        public DepositAccount()
        {
            RemainingDays = 0;
            _idOfOwner = Guid.Empty;
            _bufferOfInterest = new List<decimal>();
            Balance = 0;
            _id = Guid.Empty;
        }

        public decimal Balance { get; set; }
        public int RemainingDays { get; private set; }

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
            RemainingDays -= days;
            for (int i = 0; i < days; i++)
            {
                InterestRange interestRange = bankConfiguration.DepositAccountConfiguration.InterestRanges.FirstOrDefault(ir => ir.InRange(Balance));
                if (interestRange == null)
                {
                    _bufferOfInterest.Add(Balance * bankConfiguration.DepositAccountConfiguration.DefaultInterest / 365);
                }
                else
                {
                    _bufferOfInterest.Add(Balance * interestRange.Interest / 365);
                }
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
            return $"Deposit Account with id {_id}: Owner Id - {_idOfOwner}, Balance - {Balance}";
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
            return RemainingDays > 0 && Balance >= money;
        }
    }
}