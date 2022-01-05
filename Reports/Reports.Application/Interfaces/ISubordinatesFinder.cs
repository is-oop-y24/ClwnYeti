using System;
using System.Collections.Generic;
using Reports.Core.Entities;

namespace Reports.Application.Interfaces
{
    public interface ISubordinatesFinder
    {
        IEnumerable<Employee> FindDirectSubordinates(Guid id);
        IEnumerable<Employee> FindAllSubordinates(Guid id);
    }
}