using System;
using Banks.Classes;

namespace Banks.Interfaces
{
    public interface IAccount
    {
        public void ChargeInterests(int days, BankConfiguration bankConfiguration);
        public void CheckCommission(int days, BankConfiguration bankConfiguration);

        public void Replenish(decimal money, BankConfiguration bankConfiguration);

        public void Withdraw(decimal money, BankConfiguration bankConfiguration);

        public Guid GetId();
        public Guid GetOwnerId();
        public string GetInfo();
        public bool EqualsWith(IAccount account);
    }
}