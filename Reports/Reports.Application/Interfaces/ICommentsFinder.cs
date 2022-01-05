using System;
using System.Collections.Generic;
using Reports.Core.Entities;

namespace Reports.Application.Interfaces
{
    public interface ICommentsFinder
    {
        IEnumerable<Comment> FindCommentsOfTask(Guid taskId);
    }
}