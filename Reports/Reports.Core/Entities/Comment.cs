using System;

namespace Reports.Core.Entities
{
    public class Comment
    {
        public Comment(Guid id, ReportTask task, string content)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content), "Content is invalid");
            }
            
            Id = id;
            Task = task ?? throw new ArgumentNullException(nameof(task), "Task is invalid");
            Content = content;
        }

        private Comment()
        {
        }

        public Guid Id { get; private set; }
        public ReportTask Task { get; private set; }
        public string Content { get; private set; }
    }
}