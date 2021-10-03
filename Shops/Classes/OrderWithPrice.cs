namespace Shops.Classes
{
    public class OrderWithPrice : Order
    {
        public OrderWithPrice(string product, int count, Price price)
            : base(product, count)
        {
            Price = price;
        }

        public Price Price { get; }
    }
}