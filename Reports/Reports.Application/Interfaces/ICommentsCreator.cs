using System;
using System.Threading.Tasks;
using Reports.Core.Entities;

namespace Reports.Application.Interfaces
{
    public interface ICommentsCreator
    {
        Task<Comment> Create(Guid id, ReportTask task, string content);
    }
}