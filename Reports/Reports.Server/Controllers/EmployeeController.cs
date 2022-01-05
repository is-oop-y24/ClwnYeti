using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Interfaces;
using Reports.Core.Builders;
using Reports.Core.Entities;
using Reports.Core.Interfaces;


namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/Employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeApplicationService _applicationService;
        private readonly ISubordinatesFinder _subordinatesFinder;
        private readonly IEmployeesFinder _employeesFinder;

        public EmployeeController(IEmployeeApplicationService applicationService, IEmployeesFinder employeesFinder, ISubordinatesFinder subordinatesFinder)
        {
            _applicationService = applicationService;
            _employeesFinder = employeesFinder;
            _subordinatesFinder = subordinatesFinder;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) BadRequest();
            IEmployeeBuilder builder = new EmployeeBuilder(Guid.NewGuid(), name, null);
            return Ok(await _applicationService.Create(builder.Build()));
        }
        
        [HttpPost]
        [Route("MentorId/{mentorId:guid}")]
        public async Task<IActionResult> Create([FromQuery] string name, [FromRoute] Guid mentorId)
        {
            if (string.IsNullOrWhiteSpace(name)) BadRequest();
            Employee mentor = _employeesFinder.FindById(mentorId);
            if (mentor == null) return NotFound($"No employee with this id {mentorId}");
            IEmployeeBuilder builder = new EmployeeBuilder(Guid.NewGuid(), name, mentor);
            return Ok(await _applicationService.Create(builder.Build()));
        }
        
        [HttpPut]
        [Route("EmployeeId/{id:guid}")]
        public async Task<IActionResult> Update([FromQuery] string name, [FromQuery] Guid mentorId, [FromRoute] Guid id)
        {
            Employee employee = _employeesFinder.FindById(id);
            if (employee == null) return NotFound($"No employee with this id {id}");
            if (string.IsNullOrWhiteSpace(name) && mentorId == Guid.Empty || mentorId == id) return BadRequest();
            IEmployeeBuilder builder = new EmployeeBuilder(employee);
            if (!string.IsNullOrWhiteSpace(name))
            {
                builder.WithName(name);
            }

            if (mentorId != Guid.Empty)
            {
                Employee mentor = _employeesFinder.FindById(mentorId);
                if (mentor == null) return NotFound($"No employee with this id {mentorId}");
                builder.WithMentor(mentor);
            }

            return Ok(await _applicationService.Update(builder.Build()));
        }
        
        [HttpPut]
        [Route("EmployeeId/{id:guid}/MakeTeamLead")]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            Employee employee = _employeesFinder.FindById(id);
            if (employee == null) return NotFound($"No employee with this id {id}");
            IEmployeeBuilder builder = new EmployeeBuilder(employee);
            builder.WithMentor(null);
            return Ok(await _applicationService.Update(builder.Build()));
        }
        
        [HttpGet]
        [Route("EmployeeId/{id:guid}/Subordinates")]
        public IActionResult GetSubordinates([FromRoute] Guid id)
        {
            Employee employee = _employeesFinder.FindById(id);
            if (employee == null) return NotFound($"No employee with this id {id}");
            return Ok(_subordinatesFinder.FindAllSubordinates(id));
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            Employee employee = _employeesFinder.FindById(id);
            if (employee == null) return NotFound($"No employee with this id {id}");
            return Ok(await _applicationService.Delete(id));
        }
        
        [HttpGet]
        [Route("TeamLeads")]
        public IActionResult GetTeamLeads()
        {
            return Ok(_employeesFinder.FindAllTeamLeads());
        }
        
        [HttpGet]
        public IActionResult Find([FromQuery] string name, [FromQuery] Guid id)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Employee result = _employeesFinder.FindByName(name);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound($"No employee with this name {name}");
            }

            if (id != Guid.Empty)
            {
                Employee result = _employeesFinder.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound($"No employee with this id {id}");
            }

            return Ok(_applicationService.GetAll());
        }
    }
}