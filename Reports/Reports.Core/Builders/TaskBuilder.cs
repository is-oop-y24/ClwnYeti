using System;
using Reports.Core.Entities;
using Reports.Core.Interfaces;

namespace Reports.Core.Builders
{
    public class TaskBuilder : ITaskBuilder
    {
        private readonly Guid _id;
        private string _task;
        private Employee _employee;
        private DateTime _timeOfCreation;
        private TaskStatus _status;
        public TaskBuilder(Guid id, DateTime timeOfCreation, string task, Employee employee)
        {
            _id = id;
            _timeOfCreation = timeOfCreation;
            _task = task;
            _employee = employee;
            _status = TaskStatus.Open;
        }
        
        public TaskBuilder(ReportTask reportTask)
        {
            _id = reportTask.Id;
            _timeOfCreation = reportTask.TimeOfCreation;
            _task = reportTask.Task;
            _employee = reportTask.Employee;
            _status = reportTask.Status;
        }

        public void WithTask(string task)
        {
            _task = task;
        }

        public void WithEmployee(Employee employee)
        {
            _employee = employee;
        }

        public void WithStatus(TaskStatus status)
        {
            _status = status;
        }

        public ReportTask Build()
        {
            return new ReportTask(_id, _timeOfCreation, _task, _employee, _status);
        }
    }
}