using System;

namespace Reports.Core.Entities
{
    public class ReportTask
    {
        private string _task;

        private ReportTask()
        {
        }
        public ReportTask(Guid id, DateTime timeOfCreation, string task, Employee employee, TaskStatus status)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }

            if (timeOfCreation == default)
            {
                throw new ArgumentNullException(nameof(timeOfCreation), "Time of creation is invalid");
            }
            
            if (string.IsNullOrWhiteSpace(task))
            {
                throw new ArgumentNullException(nameof(task), "Task is invalid");
            }

            Id = id;
            TimeOfCreation = timeOfCreation;
            Employee = employee;
            Status = status;
            _task = task;
        }

        public Guid Id { get; private set; }
        public DateTime TimeOfCreation { get; private set; }
        public string Task
        {
            get => _task;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), "Task is invalid");
                }
                _task = value;
            }
        }
        public Employee Employee { get; set; }

        public TaskStatus Status { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ReportTask)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


        private bool Equals(ReportTask other)
        {
            return Id.Equals(other.Id);
        }
    }
}