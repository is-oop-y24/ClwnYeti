using System;
using System.Collections.Generic;
using System.Linq;

namespace Reports.Core.Entities
{
    public class Report
    {
        private string _description;
        private IEnumerable<ReportTask> _tasks;

        public Report(Guid id, string description, Employee employee, ReportStatus status, List<ReportTask> tasks)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }
            
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException(nameof(description), "Description is invalid");
            }
            
            if (tasks == null || tasks.Any(r => r == null))
            {
                throw new ArgumentNullException(nameof(tasks), "Tasks are invalid");
            }

            _tasks = tasks;
            Employee = employee;
            Status = status; 
            Id = id;
            _description = description;
        }

        private Report()
        {
        }
        
        public Guid Id { get; private set; }
        public IEnumerable<ReportTask> Tasks
        {
            get => _tasks;
            set
            {
                if (value == null || value.Any(r => r == null))
                {
                    throw new ArgumentNullException(nameof(value), "Tasks are invalid");
                }

                _tasks = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), "Description is invalid");
                }

                _description = value;
            }
        }
        public Employee Employee { get; set; }
        public ReportStatus Status { get; set; }
    }
}