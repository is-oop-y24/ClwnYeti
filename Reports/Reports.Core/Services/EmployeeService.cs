using System.Collections.Generic;
using Reports.Core.Entities;
using Reports.Core.Interfaces;
using Reports.Core.Statuses;

namespace Reports.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        public void Delete(Employee employee, IEnumerable<Employee> subordinates, IEnumerable<ReportTask> tasks, IEnumerable<Report> reports)
        {
            foreach (ReportTask task in tasks)
            {
                task.Employee = null;
            }
            foreach (Report report in reports)
            {
                report.Employee = null;
            }
            foreach (Employee subordinate in subordinates)
            {
                subordinate.Mentor = employee.Mentor;
            }
        }

        public Employee ChangeName(Employee employee, string name)
        {
            employee.Name = name;
            return employee;
        }

        public Employee ChangeMentor(Employee employee, Employee mentor, IEnumerable<Employee> subordinates)
        {
            foreach (Employee subordinate in subordinates)
            {
                subordinate.Mentor = employee.Mentor;
            }

            employee.Mentor = mentor;

            return employee;
        }

        public Employee MakeATeamLead(Employee employee)
        {
            employee.Mentor = null;
            return employee;
        }
    }
}