namespace Shops.Classes
{
    public class OrderWithPrice : Order
    {
        public OrderWithPrice(string product, uint count, uint price)
            : base(product, count)
        {
            Price = price;
        }

        public uint Price { get; }
    }
}