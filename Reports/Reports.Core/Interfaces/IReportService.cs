using System.Collections.Generic;
using Reports.Core.Entities;
using Reports.Core.Statuses;

namespace Reports.Core.Interfaces
{
    public interface IReportService
    {
        Report ChangeDescription(Report report, string description);
        Report ChangeStatus(Report report, ReportStatus status);
        void MakeAllReportsOfSubordinatedOutDated(IEnumerable<Report> subordinatesReports);
        Report AddTask(Report report, ReportTask task);
    }
}