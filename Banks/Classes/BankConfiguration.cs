namespace Banks.Classes
{
    public class BankConfiguration
    {
        public BankConfiguration(int interestsForDebitAccount, DepositAccountConfiguration interestsForDepositAccount, int creditLimitForCreditAccount, int commissionForCreditAccount)
        {
            InterestsForDebitAccount = interestsForDebitAccount;
            InterestsForDepositAccount = interestsForDepositAccount;
            CreditLimitForCreditAccount = creditLimitForCreditAccount;
            CommissionForCreditAccount = commissionForCreditAccount;
        }

        public int InterestsForDebitAccount { get; }
        public DepositAccountConfiguration InterestsForDepositAccount { get; }
        public int CreditLimitForCreditAccount { get; }
        public int CommissionForCreditAccount { get; }

        public static BankConfiguration Default()
        {
            return new BankConfiguration(1, DepositAccountConfiguration.Default(), 10000, 500);
        }
    }
}