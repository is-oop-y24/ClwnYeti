using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Core.Entities;
using Reports.Core.Interfaces;

namespace Reports.Application.Services
{
    public class TaskApplicationService : ITaskApplicationService
    {
        private readonly ReportsDatabaseContext _context;
        private readonly ITaskFinder _taskFinder;
        private readonly ITaskService _taskService;

        public TaskApplicationService(ReportsDatabaseContext context, ITaskFinder taskFinder, ITaskService taskService)
        {
            _context = context;
            _taskFinder = taskFinder;
            _taskService = taskService;
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
            _taskService.Update(task, changedTask);
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