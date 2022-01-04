using System;
using Reports.Core.Entities;

namespace Reports.Core.Interfaces
{
    public interface IEmployeeBuilder
    {
        void WithName(string name);
        void WithMentor(Employee mentor);
        Employee Build();
    }
}