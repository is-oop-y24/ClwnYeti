using System;
using System.Collections.Generic;
using System.Linq;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Core.Entities;

namespace Reports.Application.Services
{
    public class TaskFinder : ITaskFinder
    {
        private readonly ReportsDatabaseContext _context;
        private readonly ISubordinatesFinder _subordinatesFinder;
        private readonly IReportsFinder _reportsFinder;

        public TaskFinder(ReportsDatabaseContext context, ISubordinatesFinder subordinatesFinder, IReportsFinder reportsFinder)
        {
            _context = context;
            _subordinatesFinder = subordinatesFinder;
            _reportsFinder = reportsFinder;
        }

        public ReportTask FindById(Guid id)
        {
            return _context.Tasks.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<ReportTask> FindTasksOfEmployee(Guid employeeId)
        {
            return _context.Tasks.Where(e => e.Employee.Id == employeeId);
        }

        public IEnumerable<ReportTask> FindTasksOfReport(Guid reportId)
        {
            Report report = _reportsFinder.FindById(reportId);
            return report == null ? new List<ReportTask>() : report.Tasks;
        }

        public IEnumerable<ReportTask> FindChangedTasksByEmployee(Guid employeeId)
        {
            return new HashSet<ReportTask>(_context.Modifications
                .Where(m => m.Employee.Id == employeeId)
                .Select(m => m.Task));
        }

        public IEnumerable<ReportTask> FindTasksOfSubordinates(Guid employeeId)
        {
            IEnumerable<Employee> subordinates = _subordinatesFinder.FindAllSubordinates(employeeId);
            var tasks = new List<ReportTask>();
            foreach (Employee subordinate in subordinates)
            {
                tasks.AddRange(FindTasksOfEmployee(subordinate.Id));
            }

            return tasks;
        }

        public IEnumerable<ReportTask> FindCreatedTasksAfterTime(DateTime time)
        {
            return _context.Tasks.Where(t => t.TimeOfCreation > time);
        }

        public IEnumerable<ReportTask> FindModifiedTasksAfterTime(DateTime time)
        {
            return new HashSet<ReportTask>(_context.Modifications
                .Where(m => m.Time > time)
                .Select(m => m.Task));
        }
    }
}