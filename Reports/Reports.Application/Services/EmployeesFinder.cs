using System;
using System.Collections.Generic;
using System.Linq;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Core.Entities;

namespace Reports.Application.Services
{
    public class EmployeesFinder : IEmployeesFinder
    {
        private readonly ReportsDatabaseContext _context;

        public EmployeesFinder(ReportsDatabaseContext context)
        {
            _context = context;
        }

        public Employee FindByName(string name)
        {
            return _context.Employees.FirstOrDefault(e => e.Name == name);
        }

        public Employee FindById(Guid id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> FindAllTeamLeads()
        {
            return _context.Employees.Where(e => e.Mentor == null);
        }
    }
}