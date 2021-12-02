using System.Collections.Generic;
using Banks.Interfaces;

namespace Banks.Classes
{
    public class ClientInfo
    {
        public ClientInfo(Client client)
        {
            Client = client;
            TypesOfAccounts = new List<IAccount>();
        }

        public Client Client { get; }
        public List<IAccount> TypesOfAccounts { get; }
    }
}