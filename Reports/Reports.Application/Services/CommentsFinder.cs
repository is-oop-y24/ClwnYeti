using System;
using System.Collections.Generic;
using System.Linq;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Core.Entities;

namespace Reports.Application.Services
{
    public class CommentsFinder : ICommentsFinder
    {
        private readonly ReportsDatabaseContext _context;

        public CommentsFinder(ReportsDatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> FindCommentsOfTask(Guid taskId)
        {
            return _context.Comments.Where(c => c.Task.Id == taskId);
        }
    }
}