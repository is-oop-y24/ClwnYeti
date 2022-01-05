using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Application.Tools;
using Reports.Core.Entities;
using Reports.Core.Interfaces;
using Reports.Core.Statuses;

namespace Reports.Application.Services
{
    public class TaskApplicationService : ITaskApplicationService
    {
        private readonly ReportsDatabaseContext _context;
        private readonly ITasksFinder _tasksFinder;
        private readonly IEmployeesFinder _employeesFinder;
        private readonly ICommentsFinder _commentsFinder;
        private readonly ITaskService _taskService;

        public TaskApplicationService(ReportsDatabaseContext context, ITasksFinder tasksFinder, ITaskService taskService, ICommentsFinder commentsFinder, IEmployeesFinder employeesFinder)
        {
            _context = context;
            _tasksFinder = tasksFinder;
            _taskService = taskService;
            _commentsFinder = commentsFinder;
            _employeesFinder = employeesFinder;
        }

        public async Task<ReportTask> Create(ReportTask task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<ReportTask> Create(string task)
        {
            if (task == null || task.Trim().Equals(string.Empty)) throw new InputException();
            var reportTask = new ReportTask(Guid.NewGuid(), DateTime.Now, task, null, ReportTaskStatus.Open);
            await _context.Tasks.AddAsync(reportTask);
            await _context.SaveChangesAsync();
            return reportTask;
        }

        public async Task<ReportTask> Create(string task, Guid employeeId)
        {
            if (task == null || task.Trim().Equals(string.Empty)) throw new InputException();
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) throw new FinderException($"No employee with this id {employeeId}");
            var reportTask = new ReportTask(Guid.NewGuid(), DateTime.Now, task, employee, ReportTaskStatus.Open);
            await _context.Tasks.AddAsync(reportTask);
            await _context.SaveChangesAsync();
            return reportTask;
        }

        public async Task<Comment> CreateComment(string content, Guid id)
        {
            if (content == null || content.Trim().Equals(string.Empty)) throw new InputException();
            ReportTask reportTask = _tasksFinder.FindById(id);
            if (reportTask == null) throw new FinderException($"No task with this id {id}");
            var comment = new Comment(id, reportTask, content);
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public IEnumerable<Comment> GetComments(Guid taskId)
        {
            ReportTask task = _tasksFinder.FindById(taskId);
            if (task == null) throw new FinderException($"No task with this id {taskId}");
            return _commentsFinder.FindCommentsOfTask(taskId);
        }

        public async Task<ReportTask> Update(string task, Guid employeeId, Guid id)
        {
            if ((task == null || task.Trim().Equals(string.Empty)) && employeeId == Guid.Empty) throw new InputException();
            ReportTask reportTask = _tasksFinder.FindById(id);
            if (reportTask == null) throw new FinderException($"No task with this id {id}");
            if (!(task == null || task.Trim().Equals(string.Empty)))
            {
                _taskService.ChangeTask(reportTask, task);
            }

            if (employeeId != Guid.Empty && reportTask.Employee.Id != employeeId)
            {
                Employee employee = _employeesFinder.FindById(employeeId);
                if (employee == null) throw new FinderException($"No employee with this id {employeeId}");
                _taskService.ChangeEmployee(reportTask, employee);
            }
            var modification = new Modification(Guid.NewGuid(), reportTask.Employee, reportTask, DateTime.Now);
            await _context.Modifications.AddAsync(modification);
            await _context.SaveChangesAsync();
            return reportTask;
        }

        public async Task<ReportTask> ChangeStatusToActive(Guid id)
        {
            ReportTask reportTask = _tasksFinder.FindById(id);
            if (reportTask == null) throw new FinderException($"No task with this id {id}");
            _taskService.ChangeStatus(reportTask, ReportTaskStatus.Active);
            await _context.SaveChangesAsync();
            return reportTask;
        }

        public async Task<ReportTask> ChangeStatusToOpen(Guid id)
        {
            ReportTask reportTask = _tasksFinder.FindById(id);
            if (reportTask == null) throw new FinderException($"No task with this id {id}");
            _taskService.ChangeStatus(reportTask, ReportTaskStatus.Open);
            await _context.SaveChangesAsync();
            return reportTask;
        }

        public async Task<ReportTask> ChangeStatusToResolved(Guid id)
        {
            ReportTask reportTask = _tasksFinder.FindById(id);
            if (reportTask == null) throw new FinderException($"No task with this id {id}");
            _taskService.ChangeStatus(reportTask, ReportTaskStatus.Resolved);
            await _context.SaveChangesAsync();
            return reportTask;
        }

        public IEnumerable<ReportTask> GetModifiedTasksByEmployee(Guid employeeId)
        {
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) throw new FinderException($"No employee with this id {employeeId}");
            return _tasksFinder.FindChangedTasksByEmployee(employeeId);
        }

        public IEnumerable<ReportTask> GetTasksOwnedByEmployee(Guid employeeId)
        {
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) throw new FinderException($"No employee with this id {employeeId}");
            return _tasksFinder.FindTasksOfEmployee(employeeId);
        }

        public IEnumerable<ReportTask> GetTasksOfSubordinates(Guid employeeId)
        {
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) throw new FinderException($"No employee with this id {employeeId}");
            return _tasksFinder.FindTasksOfSubordinates(employeeId);
        }

        public IEnumerable<ReportTask> GetTasksCreatedAfter(DateTime time)
        {
            if (time == default) throw new InputException();
            return _tasksFinder.FindCreatedTasksAfterTime(time);
        }

        public IEnumerable<ReportTask> GetTasksModifiedAfter(DateTime time)
        {
            if (time == default) throw new InputException();
            return _tasksFinder.FindModifiedTasksAfterTime(time);
        }

        public ReportTask FindTask(Guid id)
        {
            if (id == Guid.Empty) throw new InputException();
            ReportTask reportTask = _tasksFinder.FindById(id);
            if (reportTask == null) throw new FinderException($"No report with this id {id}");
            return reportTask;
        }

        public IEnumerable<ReportTask> GetAll()
        {
            return _context.Tasks;
        }
    }
}