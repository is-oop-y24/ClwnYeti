using Reports.Core.Entities;
using Reports.Core.Interfaces;
using Reports.Core.Statuses;

namespace Reports.Core.Services
{
    public class TaskService : ITaskService
    {
        public ReportTask ChangeTask(ReportTask reportTask, string task)
        {
            reportTask.Task = task;
            return reportTask;
        }

        public ReportTask ChangeEmployee(ReportTask reportTask, Employee employee)
        {
            reportTask.Employee = employee;
            return reportTask;
        }

        public ReportTask ChangeStatus(ReportTask reportTask, ReportTaskStatus status)
        {
            reportTask.Status = status;
            return reportTask;
        }
    }
}