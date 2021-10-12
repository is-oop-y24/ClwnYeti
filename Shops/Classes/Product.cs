#nullable enable
namespace Shops.Classes
{
    public class Product
    {
        private int _shopId;
        public Product(string name, Price price, int shopId, int count)
        {
            Name = name;
            Price = price;
            _shopId = shopId;
            Count = count;
        }

        public Price Price { get; }
        public string Name { get; }
        public int Count { get; }

        public Product ChangePrice(Price newPrice)
        {
            return new Product(Name, newPrice, _shopId, Count);
        }

        public Product Sell(int count)
        {
            return new Product(Name, Price, _shopId, Count - count);
        }

        public Product Buy(int count)
        {
            return new Product(Name, Price, _shopId, Count + count);
        }
    }
}