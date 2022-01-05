using System.Collections.Generic;
using Reports.Core.Entities;

namespace Reports.Core.Interfaces
{
    public interface IReportService
    {
        Report Update(Report report, Report changedReport);
        void MakeAllReportsOfSubordinatedOutDated(IEnumerable<Report> subordinatesReports);
        Report AddTask(Report report, ReportTask task);
    }
}