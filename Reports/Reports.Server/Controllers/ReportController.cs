using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Interfaces;
using Reports.Application.Tools;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/Reports")]
    public class ReportController : Controller
    {
        private readonly IReportApplicationService _service;

        public ReportController(IReportApplicationService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("EmployeeId/{employeeId:guid}")]
        public async Task<IActionResult> Create([FromQuery] string description, [FromRoute] Guid employeeId)
        {
            try
            {
                return Ok(await _service.Create(description, employeeId));
            }
            catch (InputException)
            {
                return BadRequest();
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpPut]
        [Route("ReportId/{id:guid}")]
        public async Task<IActionResult> Update([FromQuery] string description, [FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.ChangeDescription(description, id));
            }
            catch (InputException)
            {
                return BadRequest();
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpGet]
        [Route("ReportId/{id:guid}/Tasks")]
        public IActionResult GetTasks([FromRoute] Guid id)
        {            
            try
            {
                return Ok(_service.FindTasksOfReport(id));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpPut]
        [Route("ReportId/{id:guid}/ChangeStatusToActive")]
        public async Task<IActionResult> ChangeStatusToActive([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.ChangeStatusToActive(id));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpPut]
        [Route("ReportId/{id:guid}/ChangeStatusToWritten")]
        public async Task<IActionResult> ChangeStatusToWritten([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.ChangeStatusToWritten(id));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpPut]
        [Route("ReportId/{id:guid}/ChangeStatusToOutdated")]
        public async Task<IActionResult> ChangeStatusToOutdated([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.ChangeStatusToOutdated(id));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Route("ReportId/{id:guid}/AddTask")]
        public async Task<IActionResult> AddTask([FromQuery] Guid taskId, [FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.AddTaskToReport(id, taskId));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpPut]
        [Route("TeamLeadId/{employeeId:guid}/MakeReportsOfSubordinatesOutdated")]
        public async Task<IActionResult> MakeReportsOfSubordinatesOutdated([FromRoute]  Guid employeeId)
        {
            try
            {
                await _service.MakeAllReportsOfSubordinatesOutDatedByTeamLead(employeeId);
                return Ok();
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
            catch (ConditionException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        [Route("EmployeeId/{employeeId:guid}/ActiveReportsOfSubordinates")]
        public IActionResult GetActiveReportsOfSubordinates([FromRoute] Guid employeeId)
        {
            try
            {
                return Ok(_service.GetActiveReportsOfSubordinates(employeeId));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpGet]
        [Route("EmployeeId/{employeeId:guid}/WrittenReportsOfSubordinates")]
        public IActionResult GetWrittenReportsOfSubordinates([FromRoute] Guid employeeId)
        {
            try
            {
                return Ok(_service.GetWrittenReportsOfSubordinates(employeeId));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpGet]
        public IActionResult Find([FromQuery] Guid id)
        {
            try
            {
                return Ok(_service.FindReport(id));
            }
            catch (InputException)
            {
                return Ok(_service.GetAll());
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}