using Banks.Tools;

namespace Banks.Classes
{
    public class CreditAccountConfiguration
    {
        public CreditAccountConfiguration()
        {
            CreditLimit = 10000;
            Commission = 500;
        }

        public CreditAccountConfiguration(decimal creditLimitForCreditAccount, decimal commissionForCreditAccount)
        {
            if (creditLimitForCreditAccount < 0)
            {
                throw new BankException("Credit limit can't be negative");
            }

            if (commissionForCreditAccount < 0)
            {
                throw new BankException("Commission can't be negative");
            }

            CreditLimit = creditLimitForCreditAccount;
            Commission = commissionForCreditAccount;
        }

        public decimal CreditLimit { get; }
        public decimal Commission { get; }
    }
}