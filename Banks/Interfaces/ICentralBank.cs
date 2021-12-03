using System;
using System.Collections.Generic;
using Banks.Classes;

namespace Banks.Interfaces
{
    public interface ICentralBank
    {
        public void SkipTime(TimeSpan timeToSkip);
        public Bank AddBank(string name);
        public Bank AddBank(string name, BankConfiguration bankConfiguration);
        public Bank GetBank(Guid bankId);

        public List<Transaction> TransferBetweenAccountsFromDifferentBanks(Guid accountFromBankId, Guid accountFromId, Guid accountToBankId, Guid accountToId, decimal money);
        public void CancelLastTransactionOfAccountInBank(Guid accountBankId, Guid accountId);
    }
}