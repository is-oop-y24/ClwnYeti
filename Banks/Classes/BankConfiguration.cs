namespace Banks.Classes
{
    public class BankConfiguration
    {
        public BankConfiguration(decimal interestForDebitAccount, DepositAccountConfiguration interestsForDepositAccount, decimal defaultInterestForDepositAccount, decimal creditLimitForCreditAccount, decimal commissionForCreditAccount, decimal criticalAmountOfMoney)
        {
            InterestForDebitAccount = interestForDebitAccount;
            InterestsForDepositAccount = interestsForDepositAccount;
            CreditLimitForCreditAccount = creditLimitForCreditAccount;
            CommissionForCreditAccount = commissionForCreditAccount;
            CriticalAmountOfMoney = criticalAmountOfMoney;
            DefaultInterestForDepositAccount = defaultInterestForDepositAccount;
        }

        public decimal InterestForDebitAccount { get; }
        public DepositAccountConfiguration InterestsForDepositAccount { get; }
        public decimal DefaultInterestForDepositAccount { get; }
        public decimal CreditLimitForCreditAccount { get; }
        public decimal CommissionForCreditAccount { get; }
        public decimal CriticalAmountOfMoney { get; }

        public static BankConfiguration Default()
        {
            return new BankConfiguration(1, DepositAccountConfiguration.Default(), 2, 10000, 500, 1000);
        }
    }
}