using System;
using System.Collections.Generic;
using Reports.Core.Entities;

namespace Reports.Application.Interfaces
{
    public interface IEmployeesFinder
    {
        Employee FindByName(string name);
        Employee FindById(Guid id);
        IEnumerable<Employee> FindAllTeamLeads();
    }
}