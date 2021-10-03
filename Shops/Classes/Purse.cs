using System;
using Shops.Tools;

namespace Shops.Classes
{
    public class Purse : Money
    {
        public Purse(string value)
            : base(value)
        {
        }

        public Purse(Money money)
            : base(money.Value)
        {
        }

        public Purse(Purse money)
            : base(money.Value)
        {
        }

        public static Purse operator -(Purse first, Price second)
        {
            if (first < second)
            {
                throw new ShopException("Not enough money to make a transaction");
            }

            string result = string.Empty;
            string max = first.Value;
            string min = string.Empty;
            for (int i = 0; i < first.Value.Length - second.Value.Length; i++)
            {
                min += '0';
            }

            min += second.Value;
            int t = 0;
            for (int i = 0; i < first.Value.Length; i++)
            {
                int s = max[^(i + 1)] - min[^(i + 1)] - t;
                if (s < 0)
                {
                    s += 10;
                    t = 1;
                }
                else
                {
                    t = 0;
                }

                result = (char)(s + 48) + result;
            }

            return new Purse(result);
        }
    }
}