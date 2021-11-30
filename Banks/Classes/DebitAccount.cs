using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Classes
{
    public class DebitAccount : IAccount
    {
        private readonly decimal _interest;
        private readonly decimal _balance;
        public DebitAccount(decimal interest)
        {
            _balance = 0;
            _interest = interest;
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
                Balance *= 1 + (_interest / 100);
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
            Balance -= money;
        }

        private bool IsUnderLimit(decimal money)
        {
            return money >= 0;
        }
    }
}