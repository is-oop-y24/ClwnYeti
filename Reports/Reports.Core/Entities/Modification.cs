using System;
using Reports.Core.Statuses;

namespace Reports.Core.Entities
{
    public class Modification
    {
        public Modification(Guid id, Employee employee, ReportTask task, DateTime time)
        {
            Id = id;
            Employee = employee;
            Task = task;
            Time = time;
        }

        public Modification()
        {
        }

        public Guid Id { get; private set; }
        public Employee Employee { get; private set; }
        public ReportTask Task { get; private set; }
        public DateTime Time { get; private set; }
    }
}