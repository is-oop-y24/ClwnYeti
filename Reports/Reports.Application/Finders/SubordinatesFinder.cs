using System;
using System.Collections.Generic;
using System.Linq;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Core.Entities;

namespace Reports.Application.Services
{
    public class SubordinatesFinder : ISubordinatesFinder
    {
        private readonly ReportsDatabaseContext _context;

        public SubordinatesFinder(ReportsDatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> FindDirectSubordinates(Guid id)
        {
            return _context.Employees.Where(e => e.Mentor.Id == id);
        }

        public IEnumerable<Employee> FindAllSubordinates(Guid id)
        {
            var subordinates = FindDirectSubordinates(id).ToList();
            var findAllSubordinates = subordinates.ToList();
            foreach (Employee subordinate in subordinates)
            {
                findAllSubordinates.AddRange(FindAllSubordinates(subordinate.Id));
            }

            return findAllSubordinates;
        }
    }
}