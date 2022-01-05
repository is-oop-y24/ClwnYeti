using Reports.Core.Entities;

namespace Reports.Core.Interfaces
{
    public interface IReportBuilder
    {
        void WithDescription(string description);
        void WithStatus(ReportStatus status);
        Report Build();
    }
}