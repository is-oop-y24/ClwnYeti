using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Interfaces;
using Reports.Application.Tools;


namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/Employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeApplicationService _service;

        public EmployeeController(IEmployeeApplicationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string name)
        {
            try
            {
                return Ok(await _service.Create(name));
            }
            catch (InputException)
            {
                return BadRequest();
            }
        }
        
        [HttpPost]
        [Route("MentorId/{mentorId:guid}")]
        public async Task<IActionResult> Create([FromQuery] string name, [FromRoute] Guid mentorId)
        {
            try
            {
                return Ok(await _service.Create(name, mentorId));
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
        [Route("EmployeeId/{id:guid}")]
        public async Task<IActionResult> Update([FromQuery] string name, [FromQuery] Guid mentorId, [FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.Update(name, mentorId, id));
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
        [Route("EmployeeId/{id:guid}/MakeTeamLead")]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.MakeATeamLead(id));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpGet]
        [Route("EmployeeId/{id:guid}/Subordinates")]
        public IActionResult GetSubordinates([FromRoute] Guid id)
        {
            try
            {
                return Ok(_service.FindAllSubordinates(id));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpGet]
        [Route("TeamLeads")]
        public IActionResult GetTeamLeads()
        {
            return Ok(_service.FindAllTeamLeads());
        }
        
        [HttpGet]
        public IActionResult Find([FromQuery] string name, [FromQuery] Guid id)
        {
            try
            {
                return Ok(_service.FindEmployee(name, id));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
            catch (InputException)
            {
                return Ok(_service.GetAll());
            }
        }
    }
}