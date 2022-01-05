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

            StringIsValidOrThrowException(content);
            
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
        
        private void StringIsValidOrThrowException(string str)
        {
            if (str == null || str.Trim().Equals(string.Empty)) throw new ArgumentNullException(nameof(str), "String is invalid");
        }
    }
}