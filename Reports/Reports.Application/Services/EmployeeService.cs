using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Core.Entities;

namespace Reports.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ReportsDatabaseContext _context;
        private readonly ISubordinatesFinder _subordinatesFinder;
        private readonly IEmployeesFinder _employeesFinder;
        private readonly ITaskFinder _taskFinder;
        private readonly IReportsFinder _reportsFinder;

        public EmployeeService(ReportsDatabaseContext context, ISubordinatesFinder subordinatesFinder, ITaskFinder taskFinder, IReportsFinder reportsFinder, IEmployeesFinder employeesFinder)
        {
            _context = context;
            _subordinatesFinder = subordinatesFinder;
            _taskFinder = taskFinder;
            _reportsFinder = reportsFinder;
            _employeesFinder = employeesFinder;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees;
        }

        public async Task<Employee> Create(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> Delete(Guid id)
        { 
            Employee employee = _employeesFinder.FindById(id);
            if (employee == null) return null;
            foreach (ReportTask task in _taskFinder.FindTasksOfEmployee(id))
            {
                task.Employee = null;
            }
            foreach (Report report in _reportsFinder.FindReportsOfEmployee(id))
            {
                report.Employee = null;
            }
            foreach (Employee subordinate in _subordinatesFinder.FindDirectSubordinates(id))
            {
                subordinate.Mentor = employee.Mentor;
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> Update(Employee changedEmployee)
        {
            Employee employee = _employeesFinder.FindById(changedEmployee.Id);
            if (employee == null) return null;
            employee.Name = changedEmployee.Name;
            if (!Equals(employee.Mentor, changedEmployee.Mentor))
            {
                foreach (Employee subordinate in _subordinatesFinder.FindDirectSubordinates(changedEmployee.Id))
                {
                    subordinate.Mentor = employee.Mentor;
                }

                employee.Mentor = changedEmployee.Mentor;
            }
            await _context.SaveChangesAsync();
            return changedEmployee;
        }
    }
}