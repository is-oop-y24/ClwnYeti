using System.Collections.Generic;
using System.Linq;
using Banks.Tools;

namespace Banks.Classes
{
    public class DepositAccountConfiguration
    {
        public DepositAccountConfiguration()
        {
            InterestRanges = new List<InterestRange>();
            DefaultInterest = 5;
        }

        public DepositAccountConfiguration(List<InterestRange> interestRanges)
        {
            if (interestRanges.Any(ir1 => interestRanges.Any(ir2 => ir1 != ir2 && InterestRange.IsHaveCollision(ir1, ir2))))
            {
                throw new BankException("Ranges of interest have a collision");
            }

            InterestRanges = interestRanges;
            DefaultInterest = 5;
        }

        public DepositAccountConfiguration(List<InterestRange> interestRanges, decimal defaultInterestForDepositAccount)
        {
            if (defaultInterestForDepositAccount < 0)
            {
                throw new BankException("Interest can't be negative");
            }

            if (interestRanges.Any(ir1 => interestRanges.Any(ir2 => ir1 != ir2 && InterestRange.IsHaveCollision(ir1, ir2))))
            {
                throw new BankException("Ranges of interest have a collision");
            }

            InterestRanges = interestRanges;
            DefaultInterest = defaultInterestForDepositAccount;
        }

        public DepositAccountConfiguration(decimal defaultInterestForDepositAccount)
        {
            if (defaultInterestForDepositAccount < 0)
            {
                throw new BankException("Interest can't be negative");
            }

            InterestRanges = new List<InterestRange>();
            DefaultInterest = defaultInterestForDepositAccount;
        }

        public decimal DefaultInterest { get; }

        public List<InterestRange> InterestRanges { get; }
    }
}