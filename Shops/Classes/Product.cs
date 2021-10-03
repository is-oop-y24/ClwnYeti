#nullable enable
namespace Shops.Classes
{
    public class Product
    {
        private int _shopId;
        public Product(string name, Price price, int shopId)
        {
            Name = name;
            Price = price;
            _shopId = shopId;
        }

        public Price Price { get; set; }
        public string Name { get; }
    }
}