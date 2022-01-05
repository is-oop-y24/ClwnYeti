using System.Collections.Generic;
using Reports.Core.Entities;
using Reports.Core.Interfaces;
using Reports.Core.Statuses;

namespace Reports.Core.Services
{
    public class ReportService : IReportService
    {
        public Report ChangeDescription(Report report, string description)
        {
            report.Description = description;
            return report;
        }

        public Report ChangeStatus(Report report, ReportStatus status)
        {
            report.Status = status;
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