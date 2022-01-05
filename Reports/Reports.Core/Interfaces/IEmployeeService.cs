using System.Collections.Generic;
using Reports.Core.Entities;

namespace Reports.Core.Interfaces
{
    public interface IEmployeeService
    {
        void Delete(Employee employee, IEnumerable<Employee> subordinates, IEnumerable<ReportTask> tasks, IEnumerable<Report> reports);
        Employee Update(Employee employee, Employee changedEmployee, IEnumerable<Employee> subordinates);
    }
}