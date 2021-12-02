using System;
using Banks.Classes;

namespace Banks.Interfaces
{
    public interface IAccount
    {
        void ChargeInterests(int days, BankConfiguration bankConfiguration);
        void CheckCommission(int days, BankConfiguration bankConfiguration);

        void Replenish(decimal money, BankConfiguration bankConfiguration);

        void Withdraw(decimal money, BankConfiguration bankConfiguration);

        Guid GetId();
        Guid GetOwnerId();
        bool EqualsWith(IAccount account);
    }
}