using System;
using System.Collections.Generic;
using Banks.Classes;
using Banks.Interfaces;

namespace Banks.Services
{
    public class CentralBank : ICentralBank
    {
        private List<Bank> _banks;
        private TimeMachine _timeMachine;

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

        public Client AddClientForBank(Client client, Bank bank)
        {
            return bank.AddClient(client);
        }

        public BankConfiguration SetAConfigurationForBank(BankConfiguration configuration, Bank bank)
        {
            bank.Configuration = configuration;
            return configuration;
        }
    }
}