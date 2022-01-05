using System;
using System.Threading.Tasks;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Core.Entities;

namespace Reports.Application.Services
{
    public class CommentsApplicationService : ICommentsApplicationService
    {
        private readonly ReportsDatabaseContext _context;

        public CommentsApplicationService(ReportsDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Comment> Create(Guid id, ReportTask task, string content)
        {
            var comment = new Comment(id, task, content);
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}