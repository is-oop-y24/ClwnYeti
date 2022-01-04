using System;
using System.Collections.Generic;
using System.Linq;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Core.Entities;

namespace Reports.Application.Services
{
    public class ReportsFinder : IReportsFinder
    {
        private readonly ReportsDatabaseContext _context;
        private readonly ISubordinatesFinder _subordinatesFinder;

        public ReportsFinder(ReportsDatabaseContext context, ISubordinatesFinder subordinatesFinder)
        {
            _context = context;
            _subordinatesFinder = subordinatesFinder;
        }

        public Report FindById(Guid id)
        {
            return _context.Reports.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Report> FindReportsOfEmployee(Guid employeeId)
        {
            return _context.Reports.Where(r => r.Employee.Id == employeeId);
        }

        public IEnumerable<Report> FindSimilarStatusReportsOfSubordinates(Guid employeeId, ReportStatus status)
        {
            IEnumerable<Employee> subordinates = _subordinatesFinder.FindAllSubordinates(employeeId);
            return _context.Reports
                .Where(r => r.Status == status && subordinates.Contains(r.Employee));
        }

        public IEnumerable<Report> FindDifferentStatusReportsOfSubordinates(Guid employeeId, ReportStatus status)
        {
            IEnumerable<Employee> subordinates = _subordinatesFinder.FindAllSubordinates(employeeId);
            return _context.Reports
                .Where(r => r.Status != status && subordinates.Contains(r.Employee));
        }
    }
}