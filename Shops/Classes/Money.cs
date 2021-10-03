using System.Linq;
using Shops.Tools;

namespace Shops.Classes
{
    public abstract class Money
    {
        protected Money(string value)
        {
            if (!IsValueOk(value)) throw new ShopException($"{value} can't be amount of money");
            Value = value;
            DeleteNulls();
        }

        protected Money(int value)
        {
            if (!IsValueOk(value.ToString())) throw new ShopException($"{value} can't be amount of money");
            Value = value.ToString();
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