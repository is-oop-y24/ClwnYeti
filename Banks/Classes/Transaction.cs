using System;

namespace Banks.Classes
{
    public class Transaction
    {
        public Transaction(Guid id, Guid accountFromBankId, Guid accountToBankId, Guid accountIdFrom, decimal money, Guid accountIdTo)
        {
            AccountIdFrom = accountIdFrom;
            Money = money;
            AccountIdTo = accountIdTo;
            Id = id;
            AccountFromBankId = accountFromBankId;
            AccountToBankId = accountToBankId;
        }

        public Guid Id { get; }

        public Guid AccountFromBankId { get; }
        public Guid AccountToBankId { get; }
        public Guid AccountIdFrom { get; }
        public Guid AccountIdTo { get; }
        public decimal Money { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Transaction)obj);
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