using System.Collections.Generic;
using System.Linq;
using Shops.Classes;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopsService
    {
        private readonly List<Shop> _shops;
        private readonly List<Customer> _customers;
        private readonly List<string> _products;

        public ShopsService()
        {
            _shops = new List<Shop>();
            _customers = new List<Customer>();
            _products = new List<string>();
        }

        public Customer AddCustomer(string name, int amountOfMoney)
        {
            _customers.Add(new Customer(name, amountOfMoney.ToString(), _customers.Count));
            return _customers[^1];
        }

        public Shop AddShop(string name, string address)
        {
            _shops.Add(new Shop(_shops.Count, name, address));
            return _shops[^1];
        }

        public string RegisterProduct(string name)
        {
            _products.Add(name);
            return _products[^1];
        }

        public bool AddProductForShop(Shop shop, string name, int count)
        {
            if (!_products.Contains(name))
            {
                throw new ShopException($"{name} isn't registered");
            }

            _shops[shop.Id] = shop.AddProduct(new Order(name, count));
            return true;
        }

        public bool AddProductForShop(Shop shop, string name, int count, int price)
        {
            if (!_products.Contains(name))
            {
                throw new ShopException($"{name} isn't registered");
            }

            _shops[shop.Id] = shop.AddProduct(new OrderWithPrice(name, count, new Price(price.ToString())));
            return true;
        }

        public bool AddProductsForShop(Shop shop, List<Order> products)
        {
            foreach (Order i in products.Where(i => !_products.Contains(i.Product)))
            {
                throw new ShopException($"{i.Product} isn't registered");
            }

            foreach (Order i in products)
            {
                _shops[shop.Id] = shop.AddProduct(i);
            }

            return true;
        }

        public bool AddProductsForShop(Shop shop, List<OrderWithPrice> products)
        {
            foreach (OrderWithPrice i in products.Where(i => !_products.Contains(i.Product)))
            {
                throw new ShopException($"{i.Product} isn't registered");
            }

            foreach (OrderWithPrice i in products)
            {
                shop.AddProduct(i);
            }

            return true;
        }

        public bool MakeTransaction(Customer customer, List<Order> products)
        {
            Price minimal = null;
            int minimalId = 0;
            var shopResponses = _shops.Select(i => i.FindProducts(products)).ToList();
            foreach (ShopResponse i in shopResponses.Where(i => minimal == null || minimal > i.Price))
            {
                minimal = i.Price;
                minimalId = i.ShopId;
            }

            if (minimal == null)
            {
                throw new ShopException("Couldn't find a shop with all products");
            }

            _shops[minimalId] = _shops[minimalId].Sell(products);
            _customers[customer.Id] = customer.Buy(shopResponses[minimalId].Price);
            return true;
        }

        public bool MakeTransaction(Customer customer, List<Order> products, Shop shop)
        {
            ShopResponse shopResponse = shop.FindProducts(products);
            if (!shopResponse.IsShopHaveProducts)
            {
                throw new ShopException($"{shop.Name} doesn't have all products");
            }

            _shops[shop.Id] = shop.Sell(products);
            _customers[customer.Id] = customer.Buy(shopResponse.Price);
            return true;
        }

        public bool SetPriceForProduct(Shop shop, string name, int price)
        {
            _shops[shop.Id] = shop.ChangePrice(name, new Price(price.ToString()));
            return true;
        }
    }
}