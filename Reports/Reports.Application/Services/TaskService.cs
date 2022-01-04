using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Core.Entities;

namespace Reports.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ReportsDatabaseContext _context;
        private readonly ITaskFinder _taskFinder;

        public TaskService(ReportsDatabaseContext context, ITaskFinder taskFinder)
        {
            _context = context;
            _taskFinder = taskFinder;
        }

        public async Task<ReportTask> Create(ReportTask task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<ReportTask> Update(ReportTask changedTask)
        {
            ReportTask task = _taskFinder.FindById(changedTask.Id);
            var modification = new Modification(Guid.NewGuid(), task.Employee, task, DateTime.Now);
            task.Employee = changedTask.Employee;
            task.Status = changedTask.Status;
            task.Task = changedTask.Task;
            await _context.Modifications.AddAsync(modification);
            await _context.SaveChangesAsync();
            return changedTask;
        }

        public IEnumerable<ReportTask> GetAll()
        {
            return _context.Tasks;
        }
    }
}