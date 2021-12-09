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
            CreditLimit = creditLimitForCreditAccount;
            Commission = commissionForCreditAccount;
        }

        public decimal CreditLimit { get; }
        public decimal Commission { get; }
    }
}