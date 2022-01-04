using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Core.Entities;

namespace Reports.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportsDatabaseContext _context;
        private readonly IReportsFinder _reportsFinder;
        private readonly IEmployeesFinder _employeesFinder;

        public ReportService(ReportsDatabaseContext context, IReportsFinder reportsFinder, IEmployeesFinder employeesFinder)
        {
            _context = context;
            _reportsFinder = reportsFinder;
            _employeesFinder = employeesFinder;
        }

        public async Task<Report> Create(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> Update(Report changedReport)
        {
            Report report = _reportsFinder.FindById(changedReport.Id);
            if (report == null) return null;
            report.Description = changedReport.Description;
            report.Status = changedReport.Status;
            report.Tasks = changedReport.Tasks;
            await _context.SaveChangesAsync();
            return changedReport;
        }

        public async Task MakeAllReportsOfSubordinatesOutDatedByTeamLead(Guid teamLeadId)
        {
            Employee employee = _employeesFinder.FindById(teamLeadId);
            if (employee == null) return;
            if (employee.Mentor != null) return;
            IEnumerable<Report> reports = _reportsFinder.FindDifferentStatusReportsOfSubordinates(teamLeadId, ReportStatus.Outdated).ToList();
            if (reports.Any(r => r.Status != ReportStatus.Written)) return;
            foreach (Report report in reports)
            {
                report.Status = ReportStatus.Outdated;
            }

            await _context.SaveChangesAsync();
        }

        public IEnumerable<Report> GetAll()
        { 
            return _context.Reports;
        }
    }
}