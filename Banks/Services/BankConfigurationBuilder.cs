using Banks.Classes;
using Banks.Tools;

namespace Banks.Services
{
    public class BankConfigurationBuilder
    {
        private decimal _criticalAmountOfMoney;
        private DebitAccountConfiguration _debitAccountConfiguration;
        private DepositAccountConfiguration _depositAccountConfiguration;
        private CreditAccountConfiguration _creditAccountConfiguration;

        public BankConfigurationBuilder()
        {
            _criticalAmountOfMoney = 1000;
            _debitAccountConfiguration = new DebitAccountConfiguration();
            _depositAccountConfiguration = new DepositAccountConfiguration();
            _creditAccountConfiguration = new CreditAccountConfiguration();
        }

        public BankConfigurationBuilder(BankConfiguration configuration)
        {
            _criticalAmountOfMoney = configuration.CriticalAmountOfMoney;
            _debitAccountConfiguration = configuration.DebitAccountConfiguration;
            _depositAccountConfiguration = configuration.DepositAccountConfiguration;
            _creditAccountConfiguration = configuration.CreditAccountConfiguration;
        }

        public void WithCriticalAmountOfMoney(decimal criticalAmountOfMoney)
        {
            if (criticalAmountOfMoney < 0) throw new BankException("Critical amount of money");
            _criticalAmountOfMoney = criticalAmountOfMoney;
        }

        public void WithDebitAccountConfiguration(DebitAccountConfiguration configuration)
        {
            _debitAccountConfiguration = configuration;
        }

        public void WithDepositAccountConfiguration(DepositAccountConfiguration configuration)
        {
            _depositAccountConfiguration = configuration;
        }

        public void WithCreditAccountConfiguration(CreditAccountConfiguration configuration)
        {
            _creditAccountConfiguration = configuration;
        }

        public BankConfiguration Build()
        {
            return new BankConfiguration(_debitAccountConfiguration, _depositAccountConfiguration, _creditAccountConfiguration, _criticalAmountOfMoney);
        }
    }
}