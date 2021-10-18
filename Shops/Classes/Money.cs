using System.Linq;
using Shops.Tools;

namespace Shops.Classes
{
    public class Money
    {
        public Money(string value)
        {
            if (!IsValueOk(value)) throw new ShopException($"{value} can't be amount of money");
            Value = value;
            DeleteNulls();
        }

        public Money(int value)
        {
            if (!IsValueOk(value.ToString())) throw new ShopException($"{value} can't be amount of money");
            Value = value.ToString();
        }

        public Money(Money other)
        {
            Value = other.Value;
        }

        public string Value { get; private set; }

        public static bool operator <(Money first, Money second)
        {
            if (first.Value.Length > second.Value.Length) return false;
            if (first.Value.Length < second.Value.Length) return true;
            for (int i = 0; i < first.Value.Length; i++)
            {
                if (first.Value[i] > second.Value[i]) return false;
                if (first.Value[i] < second.Value[i]) return true;
            }

            return false;
        }

        public static bool operator >(Money first, Money second)
        {
            if (first.Value.Length < second.Value.Length) return false;
            if (first.Value.Length > second.Value.Length) return true;
            for (int i = 0; i < first.Value.Length; i++)
            {
                if (first.Value[i] < second.Value[i]) return false;
                if (first.Value[i] > second.Value[i]) return true;
            }

            return false;
        }

        public static bool operator >=(Money first, Money second)
        {
            return !(first < second);
        }

        public static bool operator <=(Money first, Money second)
        {
            return !(first > second);
        }

        public static Money operator -(Money first, Money second)
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

            return new Money(result);
        }

        private bool IsValueOk(string value)
        {
            return value.All(i => i >= '0' && i <= '9');
        }

        private void DeleteNulls()
        {
            int i = 0;
            while (i < Value.Length && Value[i] == '0')
            {
                i++;
            }

            Value = Value.Substring(i);
        }
    }
}