using System;

namespace Banks.Interfaces
{
    public interface IAccount
    {
        void ChargeInterests(int days);
        void CheckCommission(int days);

        void Replenish(decimal money);

        void Withdraw(decimal money);
    }
}