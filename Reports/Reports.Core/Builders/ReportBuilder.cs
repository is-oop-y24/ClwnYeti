using System;
using System.Collections.Generic;
using System.Linq;
using Reports.Core.Entities;
using Reports.Core.Interfaces;

namespace Reports.Core.Builders
{
    public class ReportBuilder : IReportBuilder
    {
        private readonly Guid _id;
        private readonly List<ReportTask> _tasks;
        private string _description;
        private readonly Employee _employee;
        private ReportStatus _status;
        public ReportBuilder(Guid id, string description, Employee employee)
        {
            _id = id;
            _description = description;
            _tasks = new List<ReportTask>();
            _employee = employee;
            _status = ReportStatus.Active;
        }
        public ReportBuilder(Report report)
        {
            _id = report.Id;
            _description = report.Description;
            _tasks = report.Tasks != null ? report.Tasks.ToList() : new List<ReportTask>();
            _employee = report.Employee;
            _status = report.Status;
        }

        public void WithDescription(string description)
        {
            _description = description;
        }

        public void WithStatus(ReportStatus status)
        {
            _status = status;
        }
        

        public Report Build()
        {
            return new Report(_id, _description, _employee, _status, _tasks);
        }
    }
}