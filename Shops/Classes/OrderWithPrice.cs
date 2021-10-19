namespace Shops.Classes
{
    public class OrderWithPrice : Order
    {
        public OrderWithPrice(string product, int count, Money price)
            : base(product, count)
        {
            Price = price;
        }

        public Money Price { get; }
    }
}