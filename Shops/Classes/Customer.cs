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

        public Customer(string name, Purse amountOfMoney, int id)
        {
            Name = name;
            Id = id;
            Purse = new Purse(amountOfMoney);
        }

        public string Name { get; }
        public int Id { get; }
        private Purse Purse { get; }

        public Customer Buy(Price price)
        {
            return new Customer(Name, Purse - price, Id);
        }
    }
}