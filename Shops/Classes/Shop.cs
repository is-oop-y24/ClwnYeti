using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Classes
{
    public class Shop
    {
        private readonly Dictionary<string, Product> _products;

        public Shop(int id, string name, string address)
        {
            _products = new Dictionary<string, Product>();
            Id = id;
            Name = name;
            Address = address;
        }

        private Shop(int id, string name, string address, Dictionary<string, Product> products)
        {
            _products = products;
            Id = id;
            Name = name;
            Address = address;
        }

        public string Name { get; }
        public string Address { get; }
        public int Id { get; }

        public Shop AddProduct(OrderWithPrice order)
        {
            if (!_products.ContainsKey(order.Product))
            {
                Dictionary<string, Product> changedProducts = _products;
                changedProducts.Add(order.Product, new Product(order.Product, order.Price, Id, order.Count));
                return new Shop(Id, Name, Address, changedProducts);
            }
            else
            {
                Dictionary<string, Product> changedProducts = _products;
                changedProducts[order.Product] = _products[order.Product].Buy(order.Count);
                return new Shop(Id, Name, Address, changedProducts);
            }
        }

        public Shop AddProduct(Order order)
        {
            if (!_products.ContainsKey(order.Product))
            {
                throw new ShopException($"{order.Product} wasn't in the shop anytime\n The price isn't able to set");
            }

            Dictionary<string, Product> changedProducts = _products;
            changedProducts[order.Product] = _products[order.Product].Buy(order.Count);
            return new Shop(Id, Name, Address, changedProducts);
        }

        public ShopResponse FindProducts(List<Order> products)
        {
            var price = new Price("0");
            foreach (Order i in products)
            {
                if (!_products.ContainsKey(i.Product))
                {
                    return new ShopResponse(false, null, Id);
                }

                if (_products[i.Product].Count < i.Count)
                {
                    return new ShopResponse(false, null, Id);
                }

                price += _products[i.Product].Price * i.Count;
                break;
            }

            return new ShopResponse(true, price, Id);
        }

        public ShopResponse FindProducts(List<OrderWithPrice> products)
        {
            var price = new Price("0");
            foreach (OrderWithPrice i in products)
            {
                if (!_products.ContainsKey(i.Product))
                {
                    return new ShopResponse(false, null, Id);
                }

                if (_products[i.Product].Count < i.Count)
                {
                    return new ShopResponse(false, null, Id);
                }

                price += _products[i.Product].Price * i.Count;
                break;
            }

            return new ShopResponse(true, price, Id);
        }

        public Shop Sell(List<Order> products)
        {
            Dictionary<string, Product> changedProducts = _products;
            foreach (Order i in products)
            {
                changedProducts[i.Product] = _products[i.Product].Buy(i.Count);
            }

            return new Shop(Id, Name, Address, changedProducts);
        }

        public Shop ChangePrice(string name, Price newPrice)
        {
            if (!_products.ContainsKey(name))
            {
                throw new ShopException($"{name} wasn't in the shop anytime\n The price isn't able to set");
            }

            Dictionary<string, Product> changedProducts = _products;
            changedProducts[name] = changedProducts[name].ChangePrice(newPrice);
            return new Shop(Id, Name, Address, changedProducts);
        }
    }
}