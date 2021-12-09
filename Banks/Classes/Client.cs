using System;

namespace Banks.Classes
{
    public class Client
    {
        public Client(string name, string surname, string address, string passportId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Address = address;
            PassportId = passportId;
        }

        public Client(Guid id, string name, string surname, string address, string passportId)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Address = address;
            PassportId = passportId;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Address { get; }
        public string PassportId { get; }

        public string Notify()
        {
            return $"Client {Name} {Surname} with id {Id} was notified";
        }

        public string GetInfo()
        {
            return $"Client with id {Id}: Name - {Name}, Surname - {Surname}, Address - {Address}, PassportId - {PassportId}";
        }

        public bool IsVerified()
        {
            return Name != string.Empty && Surname != string.Empty && Address != string.Empty &&
                   PassportId != string.Empty && Id != Guid.Empty;
        }
    }
}