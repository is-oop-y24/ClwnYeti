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

        public bool InRange(decimal value)
        {
            return value >= MinValue && value <= MaxValue;
        }

        public bool IsHaveCollision(decimal minValue, decimal maxValue)
        {
            return (MinValue > minValue && MinValue < maxValue) || (MaxValue > minValue && MaxValue < maxValue);
        }
    }
}