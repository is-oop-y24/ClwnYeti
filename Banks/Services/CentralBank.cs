using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Classes;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Services
{
    public class CentralBank : ICentralBank
    {
        private readonly List<Bank> _banks;
        private readonly TimeMachine _timeMachine;

        public CentralBank()
        {
            _banks = new List<Bank>();
            _timeMachine = new TimeMachine();
        }

        public void SkipTime(TimeSpan timeToSkip)
        {
            int days = _timeMachine.HowMuchDaysToSkip(timeToSkip);
            foreach (Bank bank in _banks)
            {
                bank.SkipDays(days);
            }
        }

        public Bank AddBank(string name)
        {
            _banks.Add(new Bank(name, BankConfiguration.Default()));
            return _banks[^1];
        }

        public Bank AddBank(string name, BankConfiguration bankConfiguration)
        {
            _banks.Add(new Bank(name, bankConfiguration));
            return _banks[^1];
        }

        public Bank GetBank(Guid bankId)
        {
            foreach (Bank b in _banks.Where(b => b.Id == bankId))
            {
                return b;
            }

            throw new BankException("There is no bank with this id");
        }
    }
}