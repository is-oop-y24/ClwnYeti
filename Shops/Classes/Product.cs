using System;

#nullable enable
namespace Shops.Classes
{
    public class Product
    {
        private readonly int _shopId;
        public Product(string name, uint price, int shopId, uint count)
        {
            Name = name;
            Price = price;
            _shopId = shopId;
            Count = count;
        }

        public uint Price { get; }
        public string Name { get; }
        public uint Count { get; }

        public Product ChangePrice(uint newPrice)
        {
            return new Product(Name, newPrice, _shopId, Count);
        }

        public Product Sell(uint count)
        {
            return new Product(Name, Price, _shopId, Count - count);
        }

        public Product Buy(uint count)
        {
            return new Product(Name, Price, _shopId, Count + count);
        }
    }
}