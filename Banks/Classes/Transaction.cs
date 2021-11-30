using System;

namespace Banks.Classes
{
    public class Transaction
    {
        public Transaction(Guid accountIdFrom, decimal money, Guid accountIdTo, Guid id)
        {
            AccountIdFrom = accountIdFrom;
            Money = money;
            AccountIdTo = accountIdTo;
            Id = id;
        }

        public Transaction(decimal money, Guid accountIdTo, Guid id)
        {
            AccountIdTo = accountIdTo;
            Id = id;
            Money = money;
            AccountIdFrom = Guid.Empty;
        }

        public Transaction(Guid accountIdFrom, decimal money, Guid id)
        {
            AccountIdFrom = accountIdFrom;
            Money = money;
            Id = id;
            AccountIdTo = Guid.Empty;
        }

        public Guid Id { get; }
        public Guid AccountIdFrom { get; }
        public Guid AccountIdTo { get; }
        public decimal Money { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Transaction)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        private bool Equals(Transaction other)
        {
            return Id.Equals(other.Id);
        }
    }
}