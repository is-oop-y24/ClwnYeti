using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Core.Entities;

namespace Reports.Application.Interfaces
{
    public interface IEmployeeApplicationService
    {
        IEnumerable<Employee> GetAll();
        Task<Employee> Create(string name);
        Task<Employee> Create(string name, Guid mentorId);
        Task<Employee> Delete(Guid id);
        Task<Employee> Update(string name, Guid mentorId, Guid id);
        IEnumerable<Employee> FindAllSubordinates(Guid id);
        IEnumerable<Employee> FindAllTeamLeads();
        Employee FindEmployee(string name, Guid id);
        Task<Employee> MakeATeamLead(Guid id);
    }
}