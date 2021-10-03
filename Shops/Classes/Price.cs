using System;
using System.Collections.Generic;
using System.Text;

namespace Shops.Classes
{
    public class Price : Money
    {
        public Price(string value)
            : base(value)
        {
        }

        public Price(Money money)
            : base(money.Value)
        {
        }

        public Price(Price money)
            : base(money.Value)
        {
        }

        public static Price operator *(Price price, int count)
        {
            string first = price.Value;
            string second = string.Empty;
            var result = new StringBuilder(string.Empty);
            for (int i = 0; i < (first.Length * count.ToString().Length) + 1; i++)
            {
                result.Append('0');
            }

            for (int i = 0; i < first.Length - second.Length; i++)
            {
                second += '0';
            }

            second += count.ToString();
            for (int i = 0; i < first.Length; i++)
            {
                for (int j = 0, carry = 0; (j < count.ToString().Length) || (carry > 0); j++)
                {
                    int cur = (result[^(i + j + 1)] - 48) + ((first[^(i + 1)] - 48) * (second[^(j + 1)] - 48)) + carry;
                    result[^(i + j + 1)] = (char)((cur % 10) + 48);
                    carry = cur / 10;
                }
            }

            return new Price(result.ToString());
        }

        public static Price operator +(Price first, Price second)
        {
            string result = string.Empty;
            string forFirst = string.Empty;
            string forSecond = string.Empty;
            int maxLength = Math.Max(first.Value.Length, second.Value.Length);
            for (int i = 0; i < maxLength - first.Value.Length; i++)
            {
                forFirst += '0';
            }

            for (int i = 0; i < maxLength - second.Value.Length; i++)
            {
                forSecond += '0';
            }

            forFirst += first.Value;
            forSecond += second.Value;
            int t = 0;
            for (int i = 0; i < maxLength; i++)
            {
                int s = (forFirst[^(i + 1)] - 48) + (forSecond[^(i + 1)] - 48) + t;
                if (s >= 10)
                {
                    s -= 10;
                    t = 1;
                }
                else
                {
                    t = 0;
                }

                result = (char)(s + 48) + result;
            }

            if (t != 0)
            {
                result = '1' + result;
            }

            return new Price(result);
        }
    }
}