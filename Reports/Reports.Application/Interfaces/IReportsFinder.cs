using System;
using System.Collections.Generic;
using Reports.Core.Entities;

namespace Reports.Application.Interfaces
{
    public interface IReportsFinder
    {
        Report FindById(Guid id);
        IEnumerable<Report> FindReportsOfEmployee(Guid employeeId);
        IEnumerable<Report> FindSimilarStatusReportsOfSubordinates(Guid employeeId, ReportStatus status);
        IEnumerable<Report> FindDifferentStatusReportsOfSubordinates(Guid employeeId, ReportStatus status);
    }
}