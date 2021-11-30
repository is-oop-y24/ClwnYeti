using System.Collections.Generic;
using Banks.Interfaces;

namespace Banks.Classes
{
    public class Bank
    {
        private List<IAccount> _accounts;
        private List<Client> _clients;

        public Bank(string name, BankConfiguration bankConfiguration)
        {
            Name = name;
            Configuration = bankConfiguration;
            _accounts = new List<IAccount>();
            _clients = new List<Client>();
        }

        public BankConfiguration Configuration { get; set; }
        public string Name { get; }

        public void SkipDays(int days)
        {
            foreach (IAccount account in _accounts)
            {
                account.ChargeInterests(days);
                account.CheckCommission(days);
            }
        }

        public Client AddClient(Client client)
        {
            _clients.Add(client);
            return _clients[^1];
        }

        public IAccount AddDebitAccountForClient(Client client)
        {
            _accounts.Add(new DebitAccount(Configuration.InterestForDebitAccount, client.Id));
            return _accounts[^1];
        }

        public IAccount AddDepositAccountForClient(Client client)
        {
            _accounts.Add(new DepositAccount(Configuration.InterestsForDepositAccount, Configuration.DefaultInterestForDepositAccount, client.Id));
            return _accounts[^1];
        }

        public IAccount AddCreditAccountForClient(Client client)
        {
            _accounts.Add(new CreditAccount(Configuration.CreditLimitForCreditAccount, Configuration.CommissionForCreditAccount, client.Id));
            return _accounts[^1];
        }
    }
}