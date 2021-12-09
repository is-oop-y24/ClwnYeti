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
            Interest = interestForDebitAccount;
        }

        public decimal Interest { get; }
    }
}