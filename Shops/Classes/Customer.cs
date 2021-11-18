namespace Shops.Classes
{
    public class Customer
    {
        public Customer(string name, uint amountOfMoney, int id)
        {
            Name = name;
            Id = id;
            Balance = amountOfMoney;
        }

        public string Name { get; }
        public int Id { get; }
        private uint Balance { get; }

        public Customer Buy(uint price)
        {
            return new Customer(Name, Balance - price, Id);
        }

        public bool IsAbleToBuy(uint price)
        {
            return Balance >= price;
        }
    }
}