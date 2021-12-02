namespace Banks.Classes
{
    public class InterestRange
    {
        public InterestRange(decimal maxValue, decimal minValue, decimal interest)
        {
            MaxValue = maxValue;
            MinValue = minValue;
            Interest = interest;
        }

        public decimal MinValue { get; }
        public decimal MaxValue { get; }
        public decimal Interest { get; }

        public static bool IsHaveCollision(InterestRange first, InterestRange second)
        {
            return (first.MinValue > second.MinValue && first.MinValue < second.MaxValue) || (first.MaxValue > second.MinValue && first.MaxValue < second.MaxValue);
        }

        public bool InRange(decimal value)
        {
            return value >= MinValue && value <= MaxValue;
        }
    }
}