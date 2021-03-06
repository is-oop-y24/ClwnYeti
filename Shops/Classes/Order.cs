namespace Shops.Classes
{
    public class Order
    {
        public Order(string product, uint count)
        {
            Product = product;
            Count = count;
        }

        public Order(Order orderWithPrice)
        {
            Product = orderWithPrice.Product;
            Count = orderWithPrice.Count;
        }

        public string Product { get; }
        public uint Count { get; }
    }
}