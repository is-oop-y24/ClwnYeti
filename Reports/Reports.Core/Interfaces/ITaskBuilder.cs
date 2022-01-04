using System;
using Reports.Core.Entities;

namespace Reports.Core.Interfaces
{
    public interface ITaskBuilder
    {
        void WithTask(string task);
        void WithEmployee(Employee employee);
        void WithStatus(TaskStatus status);
        ReportTask Build();
    }
}