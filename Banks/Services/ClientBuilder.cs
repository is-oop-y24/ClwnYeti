using System;
using System.Security.Claims;
using Banks.Classes;
using Banks.Tools;

namespace Banks.Services
{
    public class ClientBuilder
    {
        private Guid _id;
        private string _name;
        private string _surname;
        private string _address;
        private string _passportId;

        public ClientBuilder()
        {
            _id = Guid.Empty;
            _name = string.Empty;
            _surname = string.Empty;
            _address = string.Empty;
            _passportId = string.Empty;
        }

        public ClientBuilder(Client client)
        {
            _id = client.Id;
            _name = client.Name;
            _surname = client.Surname;
            _address = client.Address;
            _passportId = client.PassportId;
        }

        public void WithName(string name)
        {
            if (name != string.Empty)
            {
                _name = name;
            }
            else
            {
                throw new BankException("The name is empty");
            }
        }

        public void WithSurname(string surname)
        {
            if (surname != string.Empty)
            {
                _surname = surname;
            }
            else
            {
                throw new BankException("The surname is empty");
            }
        }

        public void WithAddress(string address)
        {
            if (address != string.Empty)
            {
                _address = address;
            }
            else
            {
                throw new BankException("The surname is empty");
            }
        }

        public void WithPassportId(string passportId)
        {
            if (passportId != string.Empty)
            {
                _passportId = passportId;
            }
            else
            {
                throw new BankException("The surname is empty");
            }
        }

        public Client BuildClient()
        {
            if (_surname == string.Empty || _name == string.Empty)
            {
                throw new BankException("Can't make a client because name or surname are empty");
            }

            return _id == Guid.Empty ? new Client(_name, _surname, _address, _passportId) : new Client(_id, _name, _surname, _address, _passportId);
        }
    }
}