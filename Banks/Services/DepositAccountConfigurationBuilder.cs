using System.Collections.Generic;
using System.Linq;
using Banks.Classes;
using Banks.Tools;

namespace Banks.Services
{
    public class DepositAccountConfigurationBuilder
    {
        private decimal _defaultInterest;
        private List<InterestRange> _interestRanges;

        public DepositAccountConfigurationBuilder()
        {
            _defaultInterest = 5;
            _interestRanges = new List<InterestRange>();
        }

        public void WithDefaultInterest(decimal defaultInterest)
        {
            if (defaultInterest < 0)
            {
                throw new BankException("Interest can't be negative");
            }

            _defaultInterest = defaultInterest;
        }

        public void WithInterestRanges(List<InterestRange> interestRanges)
        {
            if (interestRanges.Any(ir1 => interestRanges.Any(ir2 => ir1 != ir2 && InterestRange.IsHaveCollision(ir1, ir2))))
            {
                throw new BankException("Ranges of interest have a collision");
            }

            _interestRanges = interestRanges;
        }

        public void SetToDefault()
        {
            _defaultInterest = 5;
            _interestRanges = new List<InterestRange>();
        }

        public DepositAccountConfiguration Build()
        {
            return new DepositAccountConfiguration(_interestRanges, _defaultInterest);
        }
    }
}