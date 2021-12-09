using System.Collections.Generic;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Classes
{
    public class ClientInfo
    {
        private Client _client;
        public ClientInfo(Client client)
        {
            _client = client;
            TypesOfAccounts = new List<IAccount>();
        }

        public Client Client
        {
            get => _client;
            set
            {
                if (_client.Id != value.Id)
                {
                    throw new BankException("Clients id are not equal");
                }

                _client = value;
            }
        }

        public List<IAccount> TypesOfAccounts { get; }
    }
}