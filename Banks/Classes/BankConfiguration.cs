using Banks.Tools;

namespace Banks.Classes
{
    public class BankConfiguration
    {
        private DebitAccountConfiguration _debitAccountConfiguration;
        private DepositAccountConfiguration _depositAccountConfiguration;
        private CreditAccountConfiguration _creditAccountConfiguration;

        public BankConfiguration(DebitAccountConfiguration debitAccountConfiguration, DepositAccountConfiguration interestsForDepositAccount, CreditAccountConfiguration creditAccountConfiguration, decimal criticalAmountOfMoney)
        {
            if (debitAccountConfiguration == null || interestsForDepositAccount == null || creditAccountConfiguration == null)
            {
                throw new BankException("Unexpected null reference");
            }

            DebitAccountConfiguration = debitAccountConfiguration;
            DepositAccountConfiguration = interestsForDepositAccount;
            CreditAccountConfiguration = creditAccountConfiguration;
            CriticalAmountOfMoney = criticalAmountOfMoney;
        }

        public DebitAccountConfiguration DebitAccountConfiguration
        {
            get => _debitAccountConfiguration;
            set => _debitAccountConfiguration = value ?? throw new BankException("Unexpected null reference");
        }

        public DepositAccountConfiguration DepositAccountConfiguration
        {
            get => _depositAccountConfiguration;
            set => _depositAccountConfiguration = value ?? throw new BankException("Unexpected null reference");
        }

        public CreditAccountConfiguration CreditAccountConfiguration
        {
            get => _creditAccountConfiguration;
            set => _creditAccountConfiguration = value ?? throw new BankException("Unexpected null reference");
        }

        public decimal CriticalAmountOfMoney { get; }

        public static BankConfiguration Default()
        {
            return new BankConfiguration(new DebitAccountConfiguration(), new DepositAccountConfiguration(), new CreditAccountConfiguration(), 1000);
        }
    }
}