using System.Collections.Generic;
using System.Linq;
using Banks.Tools;

namespace Banks.Classes
{
    public class DepositAccountConfiguration
    {
        private readonly List<InterestRange> _interestRanges;

        public DepositAccountConfiguration()
        {
            _interestRanges = new List<InterestRange>();
        }

        public static DepositAccountConfiguration Default()
        {
            var defaultValue = new DepositAccountConfiguration();
            defaultValue.AddBottomLineOfMoneyAndInterest(0, decimal.MaxValue, 2);
            return defaultValue;
        }

        public void AddBottomLineOfMoneyAndInterest(decimal startOfRange, decimal endOfRange, decimal interest)
        {
            if (_interestRanges.Any(ir => ir.IsHaveCollision(startOfRange, endOfRange)))
            {
                throw new BankException("Ranges of interests have a collision");
            }

            _interestRanges.Add(new InterestRange(startOfRange, endOfRange, interest));
        }

        public List<InterestRange> GetADepositConfiguration()
        {
            return _interestRanges;
        }
    }
}