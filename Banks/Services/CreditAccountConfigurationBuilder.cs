using Banks.Classes;
using Banks.Tools;

namespace Banks.Services
{
    public class CreditAccountConfigurationBuilder
    {
        private decimal _creditLimit;
        private decimal _commission;

        public CreditAccountConfigurationBuilder()
        {
            _creditLimit = 10000;
            _commission = 500;
        }

        public void WithCreditLimit(decimal creditLimit)
        {
            if (creditLimit < 0)
            {
                throw new BankException("Credit limit can't be negative");
            }

            _creditLimit = creditLimit;
        }

        public void WithCommission(decimal commission)
        {
            if (commission < 0)
            {
                throw new BankException("Commission can't be negative");
            }

            _commission = commission;
        }

        public void SetToDefault()
        {
            _creditLimit = 10000;
            _commission = 500;
        }

        public CreditAccountConfiguration Build()
        {
            return new CreditAccountConfiguration(_creditLimit, _commission);
        }
    }
}