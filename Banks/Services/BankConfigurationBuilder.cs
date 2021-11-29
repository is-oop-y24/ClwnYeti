using System.Collections.Generic;
using System.Linq;
using Banks.Classes;
using Banks.Tools;

namespace Banks.Services
{
    public class BankConfigurationBuilder
    {
        private int _interestsForDebitAccount;
        private DepositAccountConfiguration _interestsForDepositAccount;
        private int _creditLimitForCreditAccount;
        private int _commissionForCreditAccount;

        public BankConfigurationBuilder()
        {
            _interestsForDebitAccount = 1;
            _interestsForDepositAccount = new DepositAccountConfiguration();
            _commissionForCreditAccount = 500;
            _creditLimitForCreditAccount = 100000;
        }

        public BankConfigurationBuilder(BankConfiguration bankConfiguration)
        {
            _interestsForDebitAccount = bankConfiguration.InterestsForDebitAccount;
            _interestsForDepositAccount = bankConfiguration.InterestsForDepositAccount;
            _commissionForCreditAccount = bankConfiguration.CommissionForCreditAccount;
            _creditLimitForCreditAccount = bankConfiguration.CreditLimitForCreditAccount;
        }

        public void WithInterestsForDebitAccount(int interestsForDebitAccount)
        {
            if (interestsForDebitAccount < 0)
            {
                throw new BankException("Interests can't be negative");
            }

            _interestsForDebitAccount = interestsForDebitAccount;
        }

        public void WithInterestsForDepositAccount(DepositAccountConfiguration interestsForDepositAccount)
        {
            Dictionary<int, int> configuration = interestsForDepositAccount.GetADepositConfiguration();
            if (configuration.Keys.Any(bottomLine => bottomLine >= 0 && configuration[bottomLine] >= 0))
            {
                throw new BankException("Interest and bottom value can't be negative");
            }

            _interestsForDepositAccount = interestsForDepositAccount;
        }

        public void WithCreditLimitForCreditAccount(int creditLimitForCreditAccount)
        {
            if (creditLimitForCreditAccount < 0)
            {
                throw new BankException("Credit Limit can't be negative");
            }

            _creditLimitForCreditAccount = creditLimitForCreditAccount;
        }

        public void WithCommissionForCreditAccount(int commissionForCreditAccount)
        {
            if (commissionForCreditAccount < 0)
            {
                throw new BankException("Commission can't be negative");
            }

            _commissionForCreditAccount = commissionForCreditAccount;
        }

        public BankConfiguration BuiltBankConfiguration()
        {
            return new BankConfiguration(_interestsForDebitAccount, _interestsForDepositAccount, _creditLimitForCreditAccount, _commissionForCreditAccount);
        }
    }
}