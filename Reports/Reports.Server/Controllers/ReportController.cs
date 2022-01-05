using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Interfaces;
using Reports.Core.Builders;
using Reports.Core.Entities;
using Reports.Core.Interfaces;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/Reports")]
    public class ReportController : Controller
    {
        private readonly IReportApplicationService _applicationService;
        private readonly IEmployeesFinder _employeesFinder;
        private readonly IReportsFinder _reportsFinder;
        private readonly ITaskFinder _taskFinder;

        public ReportController(IReportApplicationService applicationService, IEmployeesFinder employeesFinder, ITaskFinder taskFinder, IReportsFinder reportsFinder)
        {
            _applicationService = applicationService;
            _employeesFinder = employeesFinder;
            _taskFinder = taskFinder;
            _reportsFinder = reportsFinder;
        }

        [HttpPost]
        [Route("EmployeeId/{employeeId:guid}")]
        public async Task<IActionResult> Create([FromQuery] string description, [FromRoute] Guid employeeId)
        {
            if (string.IsNullOrWhiteSpace(description)) BadRequest();
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) return NotFound($"No employee with this id {employeeId}");
            IReportBuilder builder = new ReportBuilder(Guid.NewGuid(), description, employee);
            return Ok(await _applicationService.Create(builder.Build()));
        }
        
        [HttpPut]
        [Route("ReportId/{id:guid}")]
        public async Task<IActionResult> Update([FromQuery] string description, [FromRoute] Guid id)
        {
            Report report = _reportsFinder.FindById(id);
            if (report == null) return NotFound($"No report with this id {id}");
            IReportBuilder builder = new ReportBuilder(report);
            builder.WithDescription(description);
            return Ok(await _applicationService.Update(builder.Build()));
        }
        
        [HttpGet]
        [Route("ReportId/{id:guid}/Tasks")]
        public IActionResult GetTasks([FromRoute] Guid id)
        {
            Report report = _reportsFinder.FindById(id);
            if (report == null) return NotFound($"No report with this id {id}");
            return Ok(_taskFinder.FindTasksOfReport(id));
        }
        
        [HttpPut]
        [Route("ReportId/{id:guid}/ChangeStatusToActive")]
        public async Task<IActionResult> ChangeStatusToActive([FromRoute] Guid id)
        {
            Report report = _reportsFinder.FindById(id);
            if (report == null) return NotFound($"No report with this id {id}");
            IReportBuilder builder = new ReportBuilder(report);
            builder.WithStatus(ReportStatus.Active);
            return Ok(await _applicationService.Update(builder.Build()));
        }
        
        [HttpPut]
        [Route("ReportId/{id:guid}/ChangeStatusToWritten")]
        public async Task<IActionResult> ChangeStatusToOpen([FromRoute] Guid id)
        {
            Report report = _reportsFinder.FindById(id);
            if (report == null) return NotFound($"No report with this id {id}");
            IReportBuilder builder = new ReportBuilder(report);
            builder.WithStatus(ReportStatus.Written);
            return Ok(await _applicationService.Update(builder.Build()));
        }
        
        [HttpPut]
        [Route("ReportId/{id:guid}/ChangeStatusToOutDated")]
        public async Task<IActionResult> ChangeStatusToOutDated([FromRoute] Guid id)
        {
            Report report = _reportsFinder.FindById(id);
            if (report == null) return NotFound($"No report with this id {id}");
            IReportBuilder builder = new ReportBuilder(report);
            builder.WithStatus(ReportStatus.Outdated);
            return Ok(await _applicationService.Update(builder.Build()));
        }

        [HttpPost]
        [Route("ReportId/{id:guid}/AddTask")]
        public async Task<IActionResult> AddTask([FromQuery] Guid taskId, [FromRoute] Guid id)
        {
            Report report = _reportsFinder.FindById(id);
            if (report == null) return NotFound($"No report with this id {id}");
            ReportTask reportTask = _taskFinder.FindById(taskId);
            if (reportTask == null) return NotFound($"No task with this id {id}");
            return Ok(await _applicationService.AddTaskToReport(id, taskId));
        }
        
        [HttpPut]
        [Route("TeamLeadId/{employeeId:guid}/MakeReportsOfSubordinatesOutdated")]
        public async Task<IActionResult> MakeReportsOfSubordinatesOutdated([FromRoute]  Guid employeeId)
        {
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) return NotFound($"No team lead with this id {employeeId}");
            if (employee.Mentor != null) return NotFound($"No team lead with this id {employeeId}");
            IEnumerable<Report> reports = _reportsFinder.FindDifferentStatusReportsOfSubordinates(employeeId, ReportStatus.Outdated).ToList();
            if (reports.Any(r => r.Status != ReportStatus.Written)) return BadRequest("Not all reports are written");
            await _applicationService.MakeAllReportsOfSubordinatesOutDatedByTeamLead(employeeId);
            return Ok();
        }
        
        [HttpGet]
        [Route("EmployeeId/{employeeId:guid}/ActiveReportsOfSubordinates")]
        public IActionResult GetActiveReportsOfSubordinates([FromRoute] Guid employeeId)
        {
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) return NotFound($"No employee with this id {employeeId}");
            return Ok(_reportsFinder.FindSimilarStatusReportsOfSubordinates(employeeId, ReportStatus.Active));
        }
        
        [HttpGet]
        [Route("EmployeeId/{employeeId:guid}/WrittenReportsOfSubordinates")]
        public IActionResult GetWrittenReportsOfSubordinates([FromRoute] Guid employeeId)
        {
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) return NotFound($"No employee with this id {employeeId}");
            return Ok(_reportsFinder.FindSimilarStatusReportsOfSubordinates(employeeId, ReportStatus.Written));
        }
        
        [HttpGet]
        public IActionResult Find([FromQuery] Guid id)
        {
            if (id != Guid.Empty)
            {
                ReportTask result = _taskFinder.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound($"No report with this id {id}");
            }

            return Ok(_applicationService.GetAll());
        }
    }
}