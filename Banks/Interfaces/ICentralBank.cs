using System;
using Banks.Classes;

namespace Banks.Interfaces
{
    public interface ICentralBank
    {
        void SkipTime(TimeSpan timeToSkip);
        Bank AddBank(string name);
        Client AddClientForBank(Client client, Guid bankId);
        void SetACreditAccountConfigurationForBank(CreditAccountConfiguration configuration, Guid bankId);
        void SetADepositAccountConfigurationForBank(DepositAccountConfiguration configuration, Guid bankId);
        void SetADebitAccountConfigurationForBank(DebitAccountConfiguration configuration, Guid bankId);
        void MakeATransferBetweenAccountsInBank(Guid bankId, Guid accountFromId, decimal money, Guid accountToId);
        void ReplenishAccountInBank(Guid bankId, decimal money, Guid accountToId);
        void WithdrawFromAccountInBank(Guid bankId, Guid accountFromId, decimal money);
    }
}