using System.Linq;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Classes
{
    public class DepositAccount : IAccount
    {
        private readonly decimal _balance;
        private DepositAccountConfiguration _configuration;
        private decimal _defaultInterest;
        public DepositAccount(DepositAccountConfiguration configuration, decimal defaultInterest)
        {
            _configuration = configuration;
            _defaultInterest = defaultInterest;
            _balance = 0;
        }

        public decimal Balance
        {
            get => _balance;
            private set
            {
                if (IsUnderLimit(value))
                {
                    Balance = value;
                }
                else
                {
                    throw new BankException("Balance on deposit card can't be negative");
                }
            }
        }

        public void ChargeInterests(int days)
        {
            for (int i = 0; i < days; i++)
            {
                InterestRange ir = _configuration.GetADepositConfiguration().FirstOrDefault(j => j.InRange(Balance));
                if (ir == null)
                {
                    Balance *= 1 + (_defaultInterest / 100);
                }
                else
                {
                    Balance *= 1 + (ir.Interest / 100);
                }
            }
        }

        public void CheckCommission(int days)
        {
        }

        public void Replenish(decimal money)
        {
            Balance += money;
        }

        public void Withdraw(decimal money)
        {
            throw new BankException("Can't withdraw money from deposit card");
        }

        private static bool IsUnderLimit(decimal money)
        {
            return money >= 0;
        }
    }
}