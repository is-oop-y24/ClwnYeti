using System;
using Banks.Classes;

namespace Banks.Interfaces
{
    public interface ICentralBank
    {
        void SkipTime(TimeSpan timeToSkip);
        Bank AddBank(string name);
        Client AddClientForBank(Client client, Bank bank);
        BankConfiguration SetAConfigurationForBank(BankConfiguration configuration, Bank bank);
    }
}