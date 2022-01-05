using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Core.Entities;
using Reports.Core.Interfaces;

namespace Reports.Application.Services
{
    public class EmployeeApplicationService : IEmployeeApplicationService
    {
        private readonly ReportsDatabaseContext _context;
        private readonly ISubordinatesFinder _subordinatesFinder;
        private readonly IEmployeesFinder _employeesFinder;
        private readonly ITaskFinder _taskFinder;
        private readonly IReportsFinder _reportsFinder;
        private readonly IEmployeeService _employeeService;

        public EmployeeApplicationService(ReportsDatabaseContext context, ISubordinatesFinder subordinatesFinder, ITaskFinder taskFinder, IReportsFinder reportsFinder, IEmployeesFinder employeesFinder, IEmployeeService employeeService)
        {
            _context = context;
            _subordinatesFinder = subordinatesFinder;
            _taskFinder = taskFinder;
            _reportsFinder = reportsFinder;
            _employeesFinder = employeesFinder;
            _employeeService = employeeService;
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
            IEnumerable<Employee> subordinates = _subordinatesFinder.FindDirectSubordinates(id);
            IEnumerable<ReportTask> tasks = _taskFinder.FindTasksOfEmployee(id);
            IEnumerable<Report> reports = _reportsFinder.FindReportsOfEmployee(id);
            _employeeService.Delete(employee, subordinates, tasks, reports);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> Update(Employee changedEmployee)
        {
            Employee employee = _employeesFinder.FindById(changedEmployee.Id);
            if (employee == null) return null;
            IEnumerable<Employee> subordinates = _subordinatesFinder.FindDirectSubordinates(changedEmployee.Id);
            _employeeService.Update(employee, changedEmployee, subordinates);
            await _context.SaveChangesAsync();
            return changedEmployee;
        }
    }
}