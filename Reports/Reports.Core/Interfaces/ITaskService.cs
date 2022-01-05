using Reports.Core.Entities;
using Reports.Core.Statuses;

namespace Reports.Core.Interfaces
{
    public interface ITaskService
    {
        ReportTask ChangeTask(ReportTask reportTask, string task);
        ReportTask ChangeEmployee(ReportTask reportTask, Employee employee);
        ReportTask ChangeStatus(ReportTask reportTask, ReportTaskStatus status);
    }
}