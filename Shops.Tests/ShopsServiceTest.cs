using System.Collections.Generic;
using NUnit.Framework;
using Shops.Classes;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class Tests
    {
        private ShopsService _shopsService;
        [SetUp]
        public void Setup()
        {
            _shopsService = new ShopsService();
        }

        [Test]
        public void AddCustomer()
        {
            Customer customer = _shopsService.AddCustomer("Миксаил Кузутов", 5000);
            Assert.IsNotNull(customer);
            Assert.AreEquals("Миксаил Кузутов", customer.Name)
        }

        [Test]
        public void AddShop()
        {
            Shop shop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
            Assert.True(shop != null && shop.Name == "Пятёрочка" && shop.Address == "Пушкинская, 15");
        }

        [Test]
        public void RegisterProduct()
        {
            string nameOfProduct = _shopsService.RegisterProduct("Кукуруза");
            Assert.True(nameOfProduct == "Кукуруза");
        }

        [Test]
        public void ShopAddProductWithoutPrice_haveBeenInShop()
        {
            Shop shop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
            string nameOfProduct = _shopsService.RegisterProduct("Кукуруза");
            var listOfOrdersWithPrices = new List<OrderWithPrice>();
            listOfOrdersWithPrices.Add(new OrderWithPrice(nameOfProduct, 0, 50));
            _shopsService.AddProductsForShop(shop, listOfOrdersWithPrices);
            var listOfOrders = new List<Order> {new Order(nameOfProduct, 5)};
            _shopsService.AddProductsForShop(shop, listOfOrders);
            Assert.True(shop.FindProducts(listOfOrders).IsShopHaveProducts);
        }
        
        [Test]
        public void ShopAddProductWithPrice_isExist()
        {
            Shop shop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
            string nameOfProduct = _shopsService.RegisterProduct("Кукуруза");
            var listOfOrders = new List<OrderWithPrice> {new OrderWithPrice(nameOfProduct, 5, 50)};
            _shopsService.AddProductsForShop(shop, listOfOrders);
            Assert.True(shop.FindProducts(listOfOrders).IsShopHaveProducts);
        }

        [Test]
        public void ShopAddProductWithoutPrice_haveNotBeenInShop()
        {
            Assert.Catch<ShopException>(() =>
            {
                Shop shop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
                string nameOfProduct = _shopsService.RegisterProduct("Кукуруза");
                var listOfOrders = new List<Order> {new Order(nameOfProduct, 5)};
                _shopsService.AddProductsForShop(shop, listOfOrders);
            });
        }

        [Test]
        public void ShopAddProduct_isNotExist()
        {
            Assert.Catch<ShopException>(() =>
            {
                Shop shop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
                _shopsService.AddProductForShop(shop, "Кукуруза", 5);
            });
        }

        [Test]
        public void ShopAddProducts_areExist()
        {
            Shop shop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
            string nameOfFirstProduct = _shopsService.RegisterProduct("Кукуруза");
            string nameOfSecondProduct = _shopsService.RegisterProduct("Молоко");
            var listOfOrders = new List<OrderWithPrice>
            {
                new OrderWithPrice(nameOfFirstProduct, 5, 50),
                new OrderWithPrice(nameOfSecondProduct, 5, 100)
            };
            Assert.True(_shopsService.AddProductsForShop(shop, listOfOrders));
        }
        
        [Test]
        public void ChangePrice_isExist()
        {
            Shop shop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
            string nameOfProduct = _shopsService.RegisterProduct("Кукуруза");
            var listOfOrders = new List<OrderWithPrice> {new OrderWithPrice(nameOfProduct, 5, 50)};
            _shopsService.AddProductsForShop(shop, listOfOrders);
            Assert.True(_shopsService.SetPriceForProduct(shop, nameOfProduct, 60));
        }
        
        [Test]
        public void ChangePrice_isNotExist()
        {
            Assert.Catch<ShopException>(() =>
            {
                Shop shop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
                string nameOfProduct = _shopsService.RegisterProduct("Кукуруза");
                _shopsService.SetPriceForProduct(shop, nameOfProduct, 60);
            });
        }
        
        [Test]
        public void MakeTransaction_ProductsAreNotExist()
        {
            Assert.Catch<ShopException>(() =>
            {
                Customer customer = _shopsService.AddCustomer("Миксаил Кузутов", 250);
                Shop firstShop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
                Shop secondShop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
                string nameOfProduct = _shopsService.RegisterProduct("Кукуруза");
                var listOfOrdersForFirst = new List<OrderWithPrice>();
                var listOfOrdersForSecond = new List<OrderWithPrice>();
                listOfOrdersForFirst.Add(new OrderWithPrice(nameOfProduct, 5, 50));
                listOfOrdersForSecond.Add(new OrderWithPrice(nameOfProduct, 5, 50));
                _shopsService.AddProductsForShop(firstShop, listOfOrdersForFirst);
                _shopsService.AddProductsForShop(secondShop, listOfOrdersForSecond);
                var listOfOrders = new List<Order> {new Order(nameOfProduct, 6)};
                _shopsService.MakeTransaction(customer, listOfOrders);
            });
        }
        
        [Test]
        public void MakeTransaction_ProductsAreExist()
        {
            Customer customer = _shopsService.AddCustomer("Миксаил Кузутов", 250);
            Shop firstShop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
            Shop secondShop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
            string nameOfProduct = _shopsService.RegisterProduct("Кукуруза");
            var listOfOrdersForFirst = new List<OrderWithPrice>();
            var listOfOrdersForSecond = new List<OrderWithPrice>();
            listOfOrdersForFirst.Add(new OrderWithPrice(nameOfProduct, 5, 50));
            listOfOrdersForSecond.Add(new OrderWithPrice(nameOfProduct, 5, 60));
            _shopsService.AddProductsForShop(firstShop, listOfOrdersForFirst);
            _shopsService.AddProductsForShop(secondShop, listOfOrdersForSecond);
            var listOfOrders = new List<Order> {new Order(nameOfProduct, 5)};
            Assert.True(_shopsService.MakeTransaction(customer, listOfOrders));
        }
        
        [Test]
        public void MakeTransaction_NotEnoughMoney()
        {
            Assert.Catch<ShopException>(() =>
            {
                Customer customer = _shopsService.AddCustomer("Миксаил Кузутов", 200);
                Shop firstShop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
                Shop secondShop = _shopsService.AddShop("Пятёрочка", "Пушкинская, 15");
                string nameOfProduct = _shopsService.RegisterProduct("Кукуруза");
                var listOfOrdersForFirst = new List<OrderWithPrice>();
                var listOfOrdersForSecond = new List<OrderWithPrice>();
                listOfOrdersForFirst.Add(new OrderWithPrice(nameOfProduct, 5, 50));
                listOfOrdersForSecond.Add(new OrderWithPrice(nameOfProduct, 5, 60));
                _shopsService.AddProductsForShop(firstShop, listOfOrdersForFirst);
                _shopsService.AddProductsForShop(secondShop, listOfOrdersForSecond);
                var listOfOrders = new List<Order> {new Order(nameOfProduct, 6)};
                _shopsService.MakeTransaction(customer, listOfOrders);
            });
        }
    }
}