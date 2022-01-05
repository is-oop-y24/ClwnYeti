using System.Collections.Generic;
using Reports.Core.Entities;
using Reports.Core.Interfaces;

namespace Reports.Core.Services
{
    public class ReportService : IReportService
    {
        public Report Update(Report report, Report changedReport)
        {
            report.Description = changedReport.Description;
            report.Employee = changedReport.Employee;
            report.Status = changedReport.Status;
            return report;
        }

        public void MakeAllReportsOfSubordinatedOutDated(IEnumerable<Report> subordinatesReports)
        {
            foreach (Report report in subordinatesReports)
            {
                report.Status = ReportStatus.Outdated;
            }
        }

        public Report AddTask(Report report, ReportTask task)
        {
            report.Tasks.Add(task);
            return report;
        }
    }
}