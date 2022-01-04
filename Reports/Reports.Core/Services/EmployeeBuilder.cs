using System;
using Reports.Core.Entities;
using Reports.Core.Interfaces;

namespace Reports.Core.Services
{
    public class EmployeeBuilder : IEmployeeBuilder
    {
        private readonly Guid _id;
        private string _name;
        private Employee _mentor;
        public EmployeeBuilder(Guid id, string name, Employee mentor)
        {
            _id = id;
            _name = name;
            _mentor = mentor;
        }
        
        public EmployeeBuilder(Employee employee)
        {
            _id = employee.Id;
            _name = employee.Name;
            _mentor = employee.Mentor;
        }
        public void WithName(string name)
        {
            _name = name;
        }

        public void WithMentor(Employee mentor)
        {
            _mentor = mentor;
        }

        public Employee Build()
        {
            return new Employee(_id, _name, _mentor);
        }
    }
}