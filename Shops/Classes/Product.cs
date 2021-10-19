#nullable enable
namespace Shops.Classes
{
    public class Product
    {
        private int _shopId;
        public Product(string name, Money price, int shopId, int count)
        {
            Name = name;
            Price = price;
            _shopId = shopId;
            Count = count;
        }

        public Money Price { get; }
        public string Name { get; }
        public int Count { get; }

        public Product ChangePrice(Money newPrice)
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