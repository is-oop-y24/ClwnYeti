using Banks.Classes;
using Banks.Tools;

namespace Banks.Services
{
    public class BankConfigurationBuilder
    {
        private decimal _interestForDebitAccount;
        private DepositAccountConfiguration _interestsForDepositAccount;
        private decimal _defaultInterestForDepositAccount;
        private decimal _creditLimitForCreditAccount;
        private decimal _commissionForCreditAccount;

        public BankConfigurationBuilder()
        {
            _interestForDebitAccount = 1;
            _interestsForDepositAccount = new DepositAccountConfiguration();
            _defaultInterestForDepositAccount = 2;
            _commissionForCreditAccount = 500;
            _creditLimitForCreditAccount = 100000;
        }

        public BankConfigurationBuilder(BankConfiguration bankConfiguration)
        {
            _interestForDebitAccount = bankConfiguration.InterestsForDebitAccount;
            _interestsForDepositAccount = bankConfiguration.InterestsForDepositAccount;
            _defaultInterestForDepositAccount = bankConfiguration.DefaultInterestForDepositAccount;
            _commissionForCreditAccount = bankConfiguration.CommissionForCreditAccount;
            _creditLimitForCreditAccount = bankConfiguration.CreditLimitForCreditAccount;
        }

        public void WithInterestsForDebitAccount(int interestForDebitAccount)
        {
            if (interestForDebitAccount < 0)
            {
                throw new BankException("Interests can't be negative");
            }

            _interestForDebitAccount = interestForDebitAccount;
        }

        public void WithInterestsForDepositAccount(DepositAccountConfiguration interestsForDepositAccount)
        {
            _interestsForDepositAccount = interestsForDepositAccount;
        }

        public void WithDefaultInterestForDepositAccount(decimal defaultInterestForDepositAccount)
        {
            if (defaultInterestForDepositAccount < 0)
            {
                throw new BankException("Interests can't be negative");
            }

            _defaultInterestForDepositAccount = defaultInterestForDepositAccount;
        }

        public void WithCreditLimitForCreditAccount(decimal creditLimitForCreditAccount)
        {
            if (creditLimitForCreditAccount < 0)
            {
                throw new BankException("Credit Limit can't be negative");
            }

            _creditLimitForCreditAccount = creditLimitForCreditAccount;
        }

        public void WithCommissionForCreditAccount(decimal commissionForCreditAccount)
        {
            if (commissionForCreditAccount < 0)
            {
                throw new BankException("Commission can't be negative");
            }

            _commissionForCreditAccount = commissionForCreditAccount;
        }

        public BankConfiguration BuiltBankConfiguration()
        {
            return new BankConfiguration(_interestForDebitAccount, _interestsForDepositAccount, _defaultInterestForDepositAccount,  _creditLimitForCreditAccount, _commissionForCreditAccount);
        }
    }
}