using System.Collections.Generic;
using Reports.Core.Entities;

namespace Reports.Core.Interfaces
{
    public interface IEmployeeService
    {
        void Delete(Employee employee, IEnumerable<Employee> subordinates, IEnumerable<ReportTask> tasks, IEnumerable<Report> reports);
        Employee ChangeName(Employee employee, string name);
        Employee ChangeMentor(Employee employee, Employee mentor, IEnumerable<Employee> subordinates);
        Employee MakeATeamLead(Employee employee);
    }
}