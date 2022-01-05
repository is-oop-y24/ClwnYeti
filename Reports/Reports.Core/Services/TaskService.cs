using Reports.Core.Entities;
using Reports.Core.Interfaces;

namespace Reports.Core.Services
{
    public class TaskService : ITaskService
    {
        public ReportTask Update(ReportTask task, ReportTask changedTask)
        {
            task.Employee = changedTask.Employee;
            task.Status = changedTask.Status;
            task.Task = changedTask.Task;
            return task;
        }
    }
}