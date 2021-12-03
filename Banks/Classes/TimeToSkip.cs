namespace Banks.Classes
{
    public class TimeToSkip
    {
        public TimeToSkip(int days, int months)
        {
            Days = days;
            Months = months;
        }

        public int Days { get; }
        public int Months { get; }
    }
}