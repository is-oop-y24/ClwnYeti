using System.Collections.Generic;
using Reports.Core.Entities;
using Reports.Core.Interfaces;

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

        public Employee Update(Employee employee, Employee changedEmployee, IEnumerable<Employee> subordinates)
        {
            employee.Name = changedEmployee.Name;
            if (Equals(employee.Mentor, changedEmployee.Mentor)) return employee;
            foreach (Employee subordinate in subordinates)
            {
                subordinate.Mentor = employee.Mentor;
            }

            employee.Mentor = changedEmployee.Mentor;

            return employee;
        }
    }
}