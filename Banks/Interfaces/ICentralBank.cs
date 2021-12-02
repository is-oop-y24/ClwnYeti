using System;
using Banks.Classes;

namespace Banks.Interfaces
{
    public interface ICentralBank
    {
        public void SkipTime(TimeSpan timeToSkip);
        public Bank AddBank(string name);
        public Bank AddBank(string name, BankConfiguration bankConfiguration);
        public Bank GetBank(Guid bankId);
    }
}