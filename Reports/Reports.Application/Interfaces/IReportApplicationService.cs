using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Core.Entities;
using Reports.Core.Statuses;

namespace Reports.Application.Interfaces
{
    public interface IReportApplicationService
    {
        Task<Report> Create(string description, Guid employeeId);
        Task<Report> ChangeDescription(string description, Guid id);
        Task<Report> ChangeStatusToActive(Guid id);
        Task<Report> ChangeStatusToWritten(Guid id);
        Task<Report> ChangeStatusToOutdated(Guid id);
        Task<Report> AddTaskToReport(Guid reportId, Guid taskId);
        IEnumerable<ReportTask> FindTasksOfReport(Guid id);
        Task MakeAllReportsOfSubordinatesOutDatedByTeamLead(Guid teamLeadId);
        Report FindReport(Guid id);
        IEnumerable<Report> GetActiveReportsOfSubordinates(Guid employeeId);
        IEnumerable<Report> GetWrittenReportsOfSubordinates(Guid employeeId);

        IEnumerable<Report> GetAll();
    }
}