using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Application.Tools;
using Reports.Core.Entities;
using Reports.Core.Interfaces;
using Reports.Core.Statuses;

namespace Reports.Application.Services
{
    public class ReportApplicationService : IReportApplicationService
    {
        private readonly ReportsDatabaseContext _context;
        private readonly IReportsFinder _reportsFinder;
        private readonly ITasksFinder _tasksFinder;
        private readonly IEmployeesFinder _employeesFinder;
        private readonly IReportService _reportService;

        public ReportApplicationService(ReportsDatabaseContext context, IReportsFinder reportsFinder, IEmployeesFinder employeesFinder, ITasksFinder tasksFinder, IReportService reportService)
        {
            _context = context;
            _reportsFinder = reportsFinder;
            _employeesFinder = employeesFinder;
            _tasksFinder = tasksFinder;
            _reportService = reportService;
        }

        public async Task<Report> Create(string description, Guid employeeId)
        {
            if (description == null || description.Trim().Equals(string.Empty)) throw new InputException();
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) throw new FinderException($"No employee with this id {employeeId}");
            var report = new Report(Guid.NewGuid(), description, employee, ReportStatus.Active, new List<ReportTask>());
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> ChangeDescription(string description, Guid id)
        {
            if (description == null || description.Trim().Equals(string.Empty)) throw new InputException();
            Report report = _reportsFinder.FindById(id);
            if (report == null) throw new FinderException($"No report with this id {id}");
            _reportService.ChangeDescription(report, description);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> ChangeStatusToActive(Guid id)
        {
            Report report = _reportsFinder.FindById(id);
            if (report == null) throw new FinderException($"No report with this id {id}");
            _reportService.ChangeStatus(report, ReportStatus.Active);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> ChangeStatusToWritten(Guid id)
        {
            Report report = _reportsFinder.FindById(id);
            if (report == null) throw new FinderException($"No report with this id {id}");
            _reportService.ChangeStatus(report, ReportStatus.Written);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> ChangeStatusToOutdated(Guid id)
        {
            Report report = _reportsFinder.FindById(id);
            if (report == null) throw new FinderException($"No report with this id {id}");
            _reportService.ChangeStatus(report, ReportStatus.Outdated);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> AddTaskToReport(Guid reportId, Guid taskId)
        {
            Report report = _reportsFinder.FindById(reportId);
            if (report == null) throw new FinderException($"No report with this id {reportId}");
            ReportTask reportTask = _tasksFinder.FindById(taskId);
            if (reportTask == null) throw new FinderException($"No task with this id {taskId}");
            _reportService.AddTask(report, reportTask);
            await _context.SaveChangesAsync();
            return report;
        }

        public IEnumerable<ReportTask> FindTasksOfReport(Guid id)
        {
            return _tasksFinder.FindTasksOfReport(id);
        }

        public async Task MakeAllReportsOfSubordinatesOutDatedByTeamLead(Guid teamLeadId)
        {
            Employee employee = _employeesFinder.FindById(teamLeadId);
            if (employee == null) throw new FinderException($"No employee with this id {teamLeadId}");
            if (employee.Mentor != null) throw new FinderException($"No team lead with this id {teamLeadId}");
            IEnumerable<Report> reports = _reportsFinder.FindDifferentStatusReportsOfSubordinates(teamLeadId, ReportStatus.Outdated).ToList();
            if (reports.Any(r => r.Status != ReportStatus.Written)) throw new ConditionException("Not all reports are written");
            _reportService.MakeAllReportsOfSubordinatedOutDated(reports);
            await _context.SaveChangesAsync();
        }

        public Report FindReport(Guid id)
        {
            if (id == Guid.Empty) throw new InputException();
            Report report = _reportsFinder.FindById(id);
            if (report == null) throw new FinderException($"No report with this id {id}");
            return report;
        }

        public IEnumerable<Report> GetActiveReportsOfSubordinates(Guid employeeId)
        {
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) throw new FinderException($"No employee with this id {employeeId}");
            return _reportsFinder.FindSimilarStatusReportsOfSubordinates(employeeId, ReportStatus.Active);
        }

        public IEnumerable<Report> GetWrittenReportsOfSubordinates(Guid employeeId)
        {
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) throw new FinderException($"No employee with this id {employeeId}");
            return _reportsFinder.FindSimilarStatusReportsOfSubordinates(employeeId, ReportStatus.Written);
        }

        public IEnumerable<Report> GetAll()
        { 
            return _context.Reports;
        }
    }
}