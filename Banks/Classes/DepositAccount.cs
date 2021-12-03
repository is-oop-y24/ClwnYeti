using System;
using System.Linq;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Classes
{
    public class DepositAccount : IAccount
    {
        private readonly Guid _id;
        private readonly Guid _idOfOwner;
        public DepositAccount(Guid idOfOwner, int remainingDays)
        {
            _idOfOwner = idOfOwner;
            RemainingDays = remainingDays;
            Balance = 0;
            _id = Guid.NewGuid();
        }

        public DepositAccount()
        {
            RemainingDays = 0;
            _idOfOwner = Guid.Empty;
            Balance = 0;
            _id = Guid.Empty;
        }

        public decimal Balance { get; set; }
        public int RemainingDays { get; private set; }

        public void ChargeInterests(int month, BankConfiguration bankConfiguration)
        {
            for (int i = 0; i < month * 30; i++)
            {
                InterestRange ir = bankConfiguration.DepositAccountConfiguration.InterestRanges.FirstOrDefault(j => j.InRange(Balance));
                if (ir == null)
                {
                    Balance *= 1 + (bankConfiguration.DepositAccountConfiguration.DefaultInterest / 365);
                }
                else
                {
                    Balance *= 1 + (ir.Interest / 365);
                }
            }
        }

        public void CheckCommission(int days, BankConfiguration bankConfiguration)
        {
            RemainingDays -= days;
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