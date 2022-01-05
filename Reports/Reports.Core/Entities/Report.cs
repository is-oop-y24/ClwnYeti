using System;
using System.Collections.Generic;
using System.Linq;

namespace Reports.Core.Entities
{
    public class Report
    {
        private string _description;

        public Report(Guid id, string description, Employee employee, ReportStatus status, List<ReportTask> tasks)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }
            
            StringIsValidOrThrowException(description);
            if (tasks == null || tasks.Any(r => r == null))
            {
                throw new ArgumentNullException(nameof(tasks), "Tasks are invalid");
            }

            Tasks = tasks;
            Employee = employee;
            Status = status; 
            Id = id;
            _description = description;
        }

        private Report()
        {
            Tasks = new List<ReportTask>();
        }
        public List<ReportTask> Tasks { get; private set; }
        public Guid Id { get; private set; }

        public string Description
        {
            get => _description;
            set
            {
                StringIsValidOrThrowException(value);
                _description = value;
            }
        }
        public Employee Employee { get; set; }
        public ReportStatus Status { get; set; }

        private void StringIsValidOrThrowException(string str)
        {
            if (str == null || str.Trim().Equals(string.Empty)) throw new ArgumentNullException(nameof(str), "String is invalid");
        }
    }
}