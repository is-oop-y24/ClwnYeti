using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Classes
{
    public class Shop
    {
        private Dictionary<Product, int> _products;

        public Shop(int id, string name, string address)
        {
            _products = new Dictionary<Product, int>();
            Id = id;
            Name = name;
            Address = address;
        }

        public int Id { get; }
        public string Name { get; }
        public string Address { get; }

        public void AddProduct(OrderWithPrice order)
        {
            bool flag = true;
            foreach (Product i in _products.Keys.Where(i => i.Name == order.Product))
            {
                _products[i] += order.Count;
                i.Price = order.Price;
                flag = false;
                break;
            }

            if (flag)
            {
                _products.Add(new Product(order.Product, order.Price, Id), order.Count);
            }
        }

        public void AddProduct(Order order)
        {
            bool flag = true;
            foreach (Product j in _products.Keys.Where(i => i.Name == order.Product))
            {
                _products[j] += order.Count;
                flag = false;
                break;
            }

            if (flag)
            {
                throw new ShopException($"{order.Product} wasn't in the shop anytime\n The price isn't able to set");
            }
        }

        public ShopResponse FindProducts(List<Order> products)
        {
            var price = new Price("0");
            foreach (Order i in products)
            {
                bool flag = true;
                foreach (Product f in _products.Keys.Where(j => j.Name == i.Product))
                {
                    if (_products[f] < i.Count)
                    {
                        return new ShopResponse(false, null, Id);
                    }

                    flag = false;
                    price += f.Price * i.Count;
                    break;
                }

                if (flag)
                {
                    return new ShopResponse(false, null, Id);
                }
            }

            return new ShopResponse(true, price, Id);
        }

        public ShopResponse FindProducts(List<OrderWithPrice> products)
        {
            var price = new Price("0");
            foreach (OrderWithPrice i in products)
            {
                bool flag = true;
                foreach (Product f in _products.Keys.Where(j => j.Name == i.Product))
                {
                    if (_products[f] < i.Count)
                    {
                        return new ShopResponse(false, null, Id);
                    }

                    flag = false;
                    price += f.Price * i.Count;
                    break;
                }

                if (flag)
                {
                    return new ShopResponse(false, null, Id);
                }
            }

            return new ShopResponse(true, price, Id);
        }

        public void Sell(List<Order> products)
        {
            foreach (Order i in products)
            {
                foreach (Product f in _products.Keys.Where(j => j.Name == i.Product))
                {
                    _products[f] -= i.Count;
                    break;
                }
            }
        }

        public void ChangePrice(string name, Price newPrice)
        {
            bool flag = true;
            foreach (Product j in _products.Keys.Where(i => i.Name == name))
            {
                j.Price = newPrice;
                flag = false;
                break;
            }

            if (flag)
            {
                throw new ShopException($"{name} wasn't in the shop anytime\n The price isn't able to set");
            }
        }
    }
}