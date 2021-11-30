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
        public DepositAccount(Guid idOfOwner)
        {
            _idOfOwner = idOfOwner;
            Balance = 0;
            _id = Guid.NewGuid();
        }

        public decimal Balance { get; private set; }

        public void ChargeInterests(int days, BankConfiguration bankConfiguration)
        {
            for (int i = 0; i < days; i++)
            {
                InterestRange ir = bankConfiguration.InterestsForDepositAccount.GetADepositConfiguration().FirstOrDefault(j => j.InRange(Balance));
                if (ir == null)
                {
                    Balance *= 1 + (bankConfiguration.DefaultInterestForDepositAccount / 100);
                }
                else
                {
                    Balance *= 1 + (ir.Interest / 100);
                }
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
            throw new BankException("Can't withdraw money from deposit card");
        }

        public Guid GetId()
        {
            return _id;
        }

        public Guid GetOwnerId()
        {
            return _idOfOwner;
        }
    }
}