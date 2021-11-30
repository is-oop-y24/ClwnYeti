namespace Banks.Classes
{
    public class BankConfiguration
    {
        public BankConfiguration(decimal interestsForDebitAccount, DepositAccountConfiguration interestsForDepositAccount, decimal defaultInterestForDepositAccount, decimal creditLimitForCreditAccount, decimal commissionForCreditAccount)
        {
            InterestsForDebitAccount = interestsForDebitAccount;
            InterestsForDepositAccount = interestsForDepositAccount;
            CreditLimitForCreditAccount = creditLimitForCreditAccount;
            CommissionForCreditAccount = commissionForCreditAccount;
            DefaultInterestForDepositAccount = defaultInterestForDepositAccount;
        }

        public decimal InterestsForDebitAccount { get; }
        public DepositAccountConfiguration InterestsForDepositAccount { get; }
        public decimal DefaultInterestForDepositAccount { get; }
        public decimal CreditLimitForCreditAccount { get; }
        public decimal CommissionForCreditAccount { get; }

        public static BankConfiguration Default()
        {
            return new BankConfiguration(1, DepositAccountConfiguration.Default(), 2, 10000, 500);
        }
    }
}