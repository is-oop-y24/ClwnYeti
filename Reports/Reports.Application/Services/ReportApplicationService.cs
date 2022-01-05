using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Core.Entities;
using Reports.Core.Interfaces;

namespace Reports.Application.Services
{
    public class ReportApplicationService : IReportApplicationService
    {
        private readonly ReportsDatabaseContext _context;
        private readonly IReportsFinder _reportsFinder;
        private readonly ITaskFinder _taskFinder;
        private readonly IEmployeesFinder _employeesFinder;
        private readonly IReportService _reportService;

        public ReportApplicationService(ReportsDatabaseContext context, IReportsFinder reportsFinder, IEmployeesFinder employeesFinder, ITaskFinder taskFinder, IReportService reportService)
        {
            _context = context;
            _reportsFinder = reportsFinder;
            _employeesFinder = employeesFinder;
            _taskFinder = taskFinder;
            _reportService = reportService;
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
            _reportService.Update(report, changedReport);
            await _context.SaveChangesAsync();
            return changedReport;
        }

        public async Task<Report> AddTaskToReport(Guid reportId, Guid taskId)
        {
            Report report = _reportsFinder.FindById(reportId);
            if (report == null) return null;
            ReportTask reportTask = _taskFinder.FindById(taskId);
            if (reportTask == null) return null;
            _reportService.AddTask(report, reportTask);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task MakeAllReportsOfSubordinatesOutDatedByTeamLead(Guid teamLeadId)
        {
            Employee employee = _employeesFinder.FindById(teamLeadId);
            if (employee == null) return;
            if (employee.Mentor != null) return;
            IEnumerable<Report> reports = _reportsFinder.FindDifferentStatusReportsOfSubordinates(teamLeadId, ReportStatus.Outdated).ToList();
            if (reports.Any(r => r.Status != ReportStatus.Written)) return;
            _reportService.MakeAllReportsOfSubordinatedOutDated(reports);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Report> GetAll()
        { 
            return _context.Reports;
        }
    }
}