namespace Banks.Classes
{
    public class BankConfiguration
    {
        public BankConfiguration(DebitAccountConfiguration debitAccountConfiguration, DepositAccountConfiguration interestsForDepositAccount, CreditAccountConfiguration creditAccountConfiguration, decimal criticalAmountOfMoney)
        {
            DebitAccountConfiguration = debitAccountConfiguration;
            DepositAccountConfiguration = interestsForDepositAccount;
            CreditAccountConfiguration = creditAccountConfiguration;
            CriticalAmountOfMoney = criticalAmountOfMoney;
        }

        public DebitAccountConfiguration DebitAccountConfiguration { get; set; }
        public DepositAccountConfiguration DepositAccountConfiguration { get; set; }
        public CreditAccountConfiguration CreditAccountConfiguration { get; set; }
        public decimal CriticalAmountOfMoney { get; }

        public static BankConfiguration Default()
        {
            return new BankConfiguration(new DebitAccountConfiguration(), new DepositAccountConfiguration(), new CreditAccountConfiguration(), 1000);
        }
    }
}