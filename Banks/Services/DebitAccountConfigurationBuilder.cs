using Banks.Classes;
using Banks.Tools;

namespace Banks.Services
{
    public class DebitAccountConfigurationBuilder
    {
        private decimal _interest;

        public DebitAccountConfigurationBuilder()
        {
            _interest = 2;
        }

        public void WithInterest(decimal interest)
        {
            if (interest < 0)
            {
                throw new BankException("Interest can't be negative");
            }

            _interest = interest;
        }

        public void SetToDefault()
        {
            _interest = 2;
        }

        public DebitAccountConfiguration Build()
        {
            return new DebitAccountConfiguration(_interest);
        }
    }
}