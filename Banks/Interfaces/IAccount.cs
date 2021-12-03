using System;
using Banks.Classes;

namespace Banks.Interfaces
{
    public interface IAccount
    {
        public void ChargeInterests(int month, BankConfiguration bankConfiguration);
        public decimal CheckCommission(BankConfiguration bankConfiguration);
        public void SkipDays(int days);

        public void Replenish(decimal money, BankConfiguration bankConfiguration);

        public void Withdraw(decimal money, BankConfiguration bankConfiguration);

        public Guid GetId();
        public Guid GetOwnerId();
        public string GetInfo();
        public bool EqualsWith(IAccount account);
        public bool CanMakeReplenish(decimal money, BankConfiguration bankConfiguration);
        public bool CanMakeWithdraw(decimal money, BankConfiguration bankConfiguration);
    }
}