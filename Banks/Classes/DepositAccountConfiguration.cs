using System.Collections.Generic;

namespace Banks.Classes
{
    public class DepositAccountConfiguration
    {
        public DepositAccountConfiguration()
        {
            InterestRanges = new List<InterestRange>();
            DefaultInterest = 5;
        }

        public DepositAccountConfiguration(List<InterestRange> interestRanges, decimal defaultInterestForDepositAccount)
        {
            InterestRanges = interestRanges;
            DefaultInterest = defaultInterestForDepositAccount;
        }

        public decimal DefaultInterest { get; }

        public IReadOnlyList<InterestRange> InterestRanges { get; }
    }
}