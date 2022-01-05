using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Interfaces;
using Reports.Application.Tools;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/Tasks")]
    public class TaskController : Controller
    {
        private readonly ITaskApplicationService _service;

        public TaskController(ITaskApplicationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string task)
        {
            try
            {
                return Ok(await _service.Create(task));
            }
            catch (InputException)
            {
                return BadRequest();
            }
        }
        
        [HttpPost]
        [Route("EmployeeId/{employeeId:guid}")]
        public async Task<IActionResult> Create([FromQuery] string task, [FromRoute] Guid employeeId)
        {
            try
            {
                return Ok(await _service.Create(task, employeeId));
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
        [Route("EmployeeId/{employeeId:guid}/Modified")]
        public IActionResult GetModifiedTasks([FromRoute] Guid employeeId)
        {
            try
            {
                return Ok(_service.GetModifiedTasksByEmployee(employeeId));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpGet]
        [Route("EmployeeId/{employeeId:guid}/OwnedBy")]
        public IActionResult GetTasksOwnedBy([FromRoute] Guid employeeId)
        {
            try
            {
                return Ok(_service.GetTasksOwnedByEmployee(employeeId));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpGet]
        [Route("EmployeeId/{employeeId:guid}/OfSubordinates")]
        public IActionResult GetTasksOfSubordinates([FromRoute] Guid employeeId)
        {
            try
            {
                return Ok(_service.GetTasksOfSubordinates(employeeId));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpGet]
        [Route("CreatedAfter")]
        public IActionResult GetTasksCreatedAfter([FromQuery] DateTime time)
        {
            try
            {
                return Ok(_service.GetTasksCreatedAfter(time));
            }
            catch (InputException)
            {
                return BadRequest();
            }
        }
        
        [HttpGet]
        [Route("ModifiedAfter")]
        public IActionResult GetTasksModifiedAfter([FromQuery] DateTime time)
        {
            try
            {
                return Ok(_service.GetTasksModifiedAfter(time));
            }
            catch (InputException)
            {
                return BadRequest();
            }
        }
        
        [HttpPut]
        [Route("TaskId/{id:guid}")]
        public async Task<IActionResult> Update([FromQuery] string task, [FromQuery] Guid employeeId, [FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.Update(task, employeeId, id));
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
        [Route("TaskId/{id:guid}/ChangeStatusToActive")]
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
        [Route("TaskId/{id:guid}/ChangeStatusToOpen")]
        public async Task<IActionResult> ChangeStatusToOpen([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.ChangeStatusToOpen(id));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpPut]
        [Route("TaskId/{id:guid}/ChangeStatusToResolved")]
        public async Task<IActionResult> ChangeStatusToResolved([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.ChangeStatusToResolved(id));
            }
            catch (FinderException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Route("TaskId/{id:guid}/CreateComment")]
        public async Task<IActionResult> CreateComment([FromQuery] string content, [FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.CreateComment(content, id));
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
        [Route("TaskId/{id:guid}/Comments")]
        public IActionResult GetComments([FromRoute] Guid id)
        {
            try
            {
                return Ok(_service.GetComments(id));
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
                return Ok(_service.FindTask(id));
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