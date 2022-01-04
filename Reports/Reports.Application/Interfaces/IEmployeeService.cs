using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Core.Entities;

namespace Reports.Application.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        Task<Employee> Create(Employee employee);
        Task<Employee> Delete(Guid id);
        Task<Employee> Update(Employee changedEmployee);
    }
}