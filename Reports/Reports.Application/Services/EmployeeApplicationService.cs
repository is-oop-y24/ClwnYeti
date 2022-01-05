using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Application.Database;
using Reports.Application.Interfaces;
using Reports.Application.Tools;
using Reports.Core.Entities;
using Reports.Core.Interfaces;

namespace Reports.Application.Services
{
    public class EmployeeApplicationService : IEmployeeApplicationService
    {
        private readonly ReportsDatabaseContext _context;
        private readonly ISubordinatesFinder _subordinatesFinder;
        private readonly IEmployeesFinder _employeesFinder;
        private readonly ITasksFinder _tasksFinder;
        private readonly IReportsFinder _reportsFinder;
        private readonly IEmployeeService _employeeService;

        public EmployeeApplicationService(ReportsDatabaseContext context, ISubordinatesFinder subordinatesFinder, ITasksFinder tasksFinder, IReportsFinder reportsFinder, IEmployeesFinder employeesFinder, IEmployeeService employeeService)
        {
            _context = context;
            _subordinatesFinder = subordinatesFinder;
            _tasksFinder = tasksFinder;
            _reportsFinder = reportsFinder;
            _employeesFinder = employeesFinder;
            _employeeService = employeeService;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees;
        }

        public async Task<Employee> Create(string name)
        {
            if (name == null || name.Trim().Equals(string.Empty)) throw new InputException();
            var employee = new Employee(Guid.NewGuid(), name, null);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> Create(string name, Guid mentorId)
        {
            if (name == null || name.Trim().Equals(string.Empty)) throw new InputException();
            Employee mentor = _employeesFinder.FindById(mentorId);
            if (mentor == null) throw new FinderException($"No employee with this id {mentorId}");
            var employee = new Employee(Guid.NewGuid(), name, mentor);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> Delete(Guid id)
        { 
            Employee employee = _employeesFinder.FindById(id);
            if (employee == null) throw new FinderException($"No employee with this id {id}");
            IEnumerable<Employee> subordinates = _subordinatesFinder.FindDirectSubordinates(id);
            IEnumerable<ReportTask> tasks = _tasksFinder.FindTasksOfEmployee(id);
            IEnumerable<Report> reports = _reportsFinder.FindReportsOfEmployee(id);
            _employeeService.Delete(employee, subordinates, tasks, reports);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> Update(string name, Guid mentorId, Guid id)
        {
            if ((name == null || name.Trim().Equals(string.Empty)) && mentorId == Guid.Empty) throw new InputException();
            Employee employee = _employeesFinder.FindById(id);
            if (employee == null) throw new FinderException($"No employee with this id {id}");
            if (!(name == null || name.Trim().Equals(string.Empty)))
            {
                _employeeService.ChangeName(employee, name);
            }

            if (mentorId != Guid.Empty && employee.Mentor.Id != mentorId)
            {
                Employee mentor = _employeesFinder.FindById(mentorId);
                if (mentor == null) throw new FinderException($"No employee with this id {mentorId}");
                IEnumerable<Employee> subordinates = _subordinatesFinder.FindDirectSubordinates(id);
                _employeeService.ChangeMentor(employee, mentor, subordinates);
            }
            await _context.SaveChangesAsync();
            return employee;
        }

        public IEnumerable<Employee> FindAllSubordinates(Guid id)
        {
            return _subordinatesFinder.FindAllSubordinates(id);
        }

        public IEnumerable<Employee> FindAllTeamLeads()
        {
            return _employeesFinder.FindAllTeamLeads();
        }

        public Employee FindEmployee(string name, Guid id)
        {
            if (!(name == null || name.Trim().Equals(string.Empty)))
            {
                Employee result = _employeesFinder.FindByName(name);
                if (result != null)
                {
                    return result;
                }

                throw new FinderException($"No employee with this name {name}");
            }

            if (id != Guid.Empty)
            {
                Employee result = _employeesFinder.FindById(id);
                if (result != null)
                {
                    return result;
                }

                throw new FinderException($"No employee with this id {id}");
            }

            throw new InputException();
        }

        public async Task<Employee> MakeATeamLead(Guid id)
        {
            Employee employee = _employeesFinder.FindById(id);
            if (employee == null) throw new FinderException($"No employee with this id {id}");
            _employeeService.MakeATeamLead(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
    }
}