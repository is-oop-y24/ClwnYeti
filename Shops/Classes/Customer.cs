using System;

namespace Shops.Classes
{
    public class Customer
    {
        public Customer(string name, string amountOfMoney, int id)
        {
            Name = name;
            Id = id;
            Purse = new Money(amountOfMoney);
        }

        public Customer(string name, Money amountOfMoney, int id)
        {
            Name = name;
            Id = id;
            Purse = new Money(amountOfMoney);
        }

        public string Name { get; }
        public int Id { get; }
        private Money Purse { get; }

        public Customer Buy(Price price)
        {
            return new Customer(Name, Purse - price, Id);
        }
    }
}