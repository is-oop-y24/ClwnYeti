using Banks.Interfaces;

namespace Banks.Classes
{
    public class CreditAccount : IAccount
    {
        private readonly decimal _creditLimit;
        private readonly decimal _commission;
        private readonly decimal _balance;

        public CreditAccount(decimal creditLimit, decimal commission)
        {
            _balance = 0;
            _creditLimit = creditLimit;
            _commission = commission;
        }

        public CreditAccount(decimal balance)
        {
            Balance = balance;
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
                    if (value > 0)
                    {
                        Balance = _creditLimit;
                    }
                    else
                    {
                        Balance = -_creditLimit;
                    }
                }
            }
        }

        public void ChargeInterests(int days)
        {
        }

        public void CheckCommission(int days)
        {
            if (Balance < 0)
            {
                Balance -= days * _commission;
            }
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
            return money >= -_creditLimit && money <= _creditLimit;
        }
    }
}