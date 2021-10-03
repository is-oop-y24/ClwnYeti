using System;

namespace Shops.Classes
{
    public class Customer
    {
        public Customer(string name, string amountOfMoney, int id)
        {
            Name = name;
            Id = id;
            Purse = new Purse(amountOfMoney);
        }

        public string Name { get; }
        public int Id { get; }
        private Purse Purse { get; set; }

        public void Buy(Price price)
        {
            Purse -= price;
        }
    }
}