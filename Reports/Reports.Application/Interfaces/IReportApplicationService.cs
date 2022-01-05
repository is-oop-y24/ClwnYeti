using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Core.Entities;

namespace Reports.Application.Interfaces
{
    public interface IReportApplicationService
    {
        Task<Report> Create(Report report);
        Task<Report> Update(Report changedReport);
        Task<Report> AddTaskToReport(Guid reportId, Guid taskId);
        Task MakeAllReportsOfSubordinatesOutDatedByTeamLead(Guid teamLeadId);

        IEnumerable<Report> GetAll();
    }
}