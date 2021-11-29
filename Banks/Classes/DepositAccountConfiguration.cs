using System.Collections.Generic;

namespace Banks.Classes
{
    public class DepositAccountConfiguration
    {
        private readonly Dictionary<int, int> _bottomLineOfMoneyAndInterest;

        public DepositAccountConfiguration()
        {
            _bottomLineOfMoneyAndInterest = new Dictionary<int, int>();
        }

        public static DepositAccountConfiguration Default()
        {
            var defaultValue = new DepositAccountConfiguration();
            defaultValue.AddBottomLineOfMoneyAndInterest(0, 5);
            return defaultValue;
        }

        public void AddBottomLineOfMoneyAndInterest(int amountOfMoney, int interest)
        {
            _bottomLineOfMoneyAndInterest[amountOfMoney] = interest;
        }

        public Dictionary<int, int> GetADepositConfiguration()
        {
            return _bottomLineOfMoneyAndInterest;
        }
    }
}