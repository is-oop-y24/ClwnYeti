using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Core.Entities;

namespace Reports.Application.Interfaces
{
    public interface ITaskApplicationService
    {
        Task<ReportTask> Create(string task);
        Task<ReportTask> Create(string task, Guid employeeId);
        Task<Comment> CreateComment(string content, Guid id);
        IEnumerable<Comment> GetComments(Guid taskId);
        Task<ReportTask> Update(string task, Guid employeeId, Guid id);
        Task<ReportTask> ChangeStatusToActive(Guid id);
        Task<ReportTask> ChangeStatusToOpen(Guid id);
        Task<ReportTask> ChangeStatusToResolved(Guid id);
        IEnumerable<ReportTask> GetModifiedTasksByEmployee(Guid employeeId);
        IEnumerable<ReportTask> GetTasksOwnedByEmployee(Guid employeeId);
        IEnumerable<ReportTask> GetTasksOfSubordinates(Guid employeeId);
        IEnumerable<ReportTask> GetTasksCreatedAfter(DateTime time);
        IEnumerable<ReportTask> GetTasksModifiedAfter(DateTime time);
        ReportTask FindTask(Guid id);
        IEnumerable<ReportTask> GetAll();
    }
}