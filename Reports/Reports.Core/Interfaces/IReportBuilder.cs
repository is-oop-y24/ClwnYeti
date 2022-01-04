using Reports.Core.Entities;

namespace Reports.Core.Interfaces
{
    public interface IReportBuilder
    {
        void WithDescription(string description);
        void AddTask(ReportTask task);
        void WithStatus(ReportStatus status);
        Report Build();
    }
}