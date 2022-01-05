using System;
using System.Collections.Generic;

using Reports.Core.Entities;
using Reports.Core.Statuses;

namespace Reports.Application.Interfaces
{
    public interface ITasksFinder
    {
        ReportTask FindById(Guid id);
        IEnumerable<ReportTask> FindTasksOfEmployee(Guid employeeId);
        IEnumerable<ReportTask> FindTasksOfReport(Guid reportId);
        IEnumerable<ReportTask> FindChangedTasksByEmployee(Guid employeeId);
        IEnumerable<ReportTask> FindTasksOfSubordinates(Guid employeeId);
        IEnumerable<ReportTask> FindCreatedTasksAfterTime(DateTime time);
        IEnumerable<ReportTask> FindModifiedTasksAfterTime(DateTime time);
    }
}