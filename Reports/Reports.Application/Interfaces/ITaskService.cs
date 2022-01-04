using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Core.Entities;

namespace Reports.Application.Interfaces
{
    public interface ITaskService
    {
        Task<ReportTask> Create(ReportTask task);
        Task<ReportTask> Update(ReportTask changedTask);
        IEnumerable<ReportTask> GetAll();
    }
}