using System;

namespace Reports.Core.Entities
{
    public class Employee
    {
        private string _name;
        public Employee(Guid id, string name, Employee mentor)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }

            StringIsValidOrThrowException(name);
            Id = id;
            _name = name;
            Mentor = mentor;
        }

        private Employee()
        {
        }

        public Guid Id { get; private set; }

        public string Name
        {
            get => _name;
            set
            {
                StringIsValidOrThrowException(value);
                _name = value;
            }
        }
        public Employee Mentor { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Employee)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


        private bool Equals(Employee other)
        {
            return Id.Equals(other.Id);
        }

        private void StringIsValidOrThrowException(string str)
        {
            if (str == null || str.Trim().Equals(string.Empty)) throw new ArgumentNullException(nameof(str), "String is invalid");
        }
    }
}