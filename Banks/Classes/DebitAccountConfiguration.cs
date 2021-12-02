using Banks.Tools;

namespace Banks.Classes
{
    public class DebitAccountConfiguration
    {
        public DebitAccountConfiguration()
        {
            Interest = 2;
        }

        public DebitAccountConfiguration(decimal interestForDebitAccount)
        {
            if (interestForDebitAccount < 0)
            {
                throw new BankException("Interest can't be negative");
            }
        }

        public decimal Interest { get; }
    }
}