using Reports.Core.Entities;

namespace Reports.Core.Interfaces
{
    public interface ITaskService
    {
        ReportTask Update(ReportTask task, ReportTask changedTask);
    }
}