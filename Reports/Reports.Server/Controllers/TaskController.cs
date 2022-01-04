using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Interfaces;
using Reports.Core.Entities;
using Reports.Core.Interfaces;
using Reports.Core.Services;
using TaskStatus = Reports.Core.Entities.TaskStatus;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/Tasks")]
    public class TaskController : Controller
    {
        private readonly ITaskService _service;
        private readonly IEmployeesFinder _employeesFinder;
        private readonly ICommentsCreator _commentsCreator;
        private readonly ICommentsFinder _commentsFinder;
        private readonly ITaskFinder _taskFinder;

        public TaskController(ITaskService service, IEmployeesFinder employeesFinder, ITaskFinder taskFinder, ICommentsCreator commentsCreator, ICommentsFinder commentsFinder)
        {
            _service = service;
            _employeesFinder = employeesFinder;
            _taskFinder = taskFinder;
            _commentsCreator = commentsCreator;
            _commentsFinder = commentsFinder;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string task)
        {
            if (string.IsNullOrWhiteSpace(task)) BadRequest();
            ITaskBuilder builder = new TaskBuilder(Guid.NewGuid(), DateTime.Now, task, null);
            return Ok(await _service.Create(builder.Build()));
        }
        
        [HttpPost]
        [Route("EmployeeId/{employeeId:guid}")]
        public async Task<IActionResult> Create([FromQuery] string task, [FromRoute] Guid employeeId)
        {
            if (string.IsNullOrWhiteSpace(task)) BadRequest();
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) return NotFound($"No employee with this id {employeeId}");
            ITaskBuilder builder = new TaskBuilder(Guid.NewGuid(), DateTime.Now, task, employee);
            return Ok(await _service.Create(builder.Build()));
        }
        
        [HttpGet]
        [Route("EmployeeId/{employeeId:guid}/Modified")]
        public IActionResult GetModifiedTasks([FromRoute] Guid employeeId)
        {
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) return NotFound($"No employee with this id {employeeId}");
            return Ok(_taskFinder.FindChangedTasksByEmployee(employeeId));
        }
        
        [HttpGet]
        [Route("EmployeeId/{employeeId:guid}/OwnedBy")]
        public IActionResult GetTaskOwnedBy([FromRoute] Guid employeeId)
        {
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) return NotFound($"No employee with this id {employeeId}");
            return Ok(_taskFinder.FindTasksOfEmployee(employeeId));
        }
        
        [HttpGet]
        [Route("EmployeeId/{employeeId:guid}/OfSubordinates")]
        public IActionResult GetTaskOfSubordinates([FromRoute] Guid employeeId)
        {
            Employee employee = _employeesFinder.FindById(employeeId);
            if (employee == null) return NotFound($"No employee with this id {employeeId}");
            return Ok(_taskFinder.FindTasksOfSubordinates(employeeId));
        }
        
        [HttpGet]
        [Route("CreatedAfter")]
        public IActionResult GetTaskCreatedAfter([FromQuery] DateTime time)
        {
            if (time == default) return BadRequest();
            return Ok(_taskFinder.FindCreatedTasksAfterTime(time));
        }
        
        [HttpGet]
        [Route("ModifiedAfter")]
        public IActionResult GetTaskModifiedAfter([FromQuery] DateTime time)
        {
            if (time == default) return BadRequest();
            return Ok(_taskFinder.FindModifiedTasksAfterTime(time));
        }
        
        [HttpPut]
        [Route("TaskId/{id:guid}")]
        public async Task<IActionResult> Update([FromQuery] string task, [FromQuery] Guid employeeId, [FromRoute] Guid id)
        {
            ReportTask reportTask = _taskFinder.FindById(id);
            if (reportTask == null) return NotFound($"No task with this id {id}");
            if (string.IsNullOrWhiteSpace(task) && employeeId == Guid.Empty) return BadRequest();
            ITaskBuilder builder = new TaskBuilder(reportTask);
            if (!string.IsNullOrWhiteSpace(task))
            {
                builder.WithTask(task);
            }

            if (employeeId != Guid.Empty)
            {
                Employee employee = _employeesFinder.FindById(employeeId);
                if (employee == null) return NotFound($"No employee with this id {employeeId}");
                builder.WithEmployee(employee);
            }

            return Ok(await _service.Update(builder.Build()));
        }
        
        [HttpPut]
        [Route("TaskId/{id:guid}/ChangeStatusToActive")]
        public async Task<IActionResult> ChangeStatusToActive([FromRoute] Guid id)
        {
            ReportTask task = _taskFinder.FindById(id);
            if (task == null) return NotFound($"No task with this id {id}");
            ITaskBuilder builder = new TaskBuilder(task);
            builder.WithStatus(TaskStatus.Active);
            return Ok(await _service.Update(builder.Build()));
        }
        
        [HttpPut]
        [Route("TaskId/{id:guid}/ChangeStatusToOpen")]
        public async Task<IActionResult> ChangeStatusToOpen([FromRoute] Guid id)
        {
            ReportTask task = _taskFinder.FindById(id);
            if (task == null) return NotFound($"No task with this id {id}");
            ITaskBuilder builder = new TaskBuilder(task);
            builder.WithStatus(TaskStatus.Open);
            return Ok(await _service.Update(builder.Build()));
        }
        
        [HttpPut]
        [Route("TaskId/{id:guid}/ChangeStatusToResolved")]
        public async Task<IActionResult> ChangeStatusToResolved([FromRoute] Guid id)
        {
            ReportTask task = _taskFinder.FindById(id);
            if (task == null) return NotFound($"No task with this id {id}");
            ITaskBuilder builder = new TaskBuilder(task);
            builder.WithStatus(TaskStatus.Resolved);
            return Ok(await _service.Update(builder.Build()));
        }

        [HttpPost]
        [Route("TaskId/{id:guid}/CreateComment")]
        public async Task<IActionResult> CreateComment([FromQuery] string content, [FromRoute] Guid id)
        {
            ReportTask task = _taskFinder.FindById(id);
            if (task == null) return NotFound($"No task with this id {id}");
            if (!string.IsNullOrWhiteSpace(content))
            {
                return Ok(await _commentsCreator.Create(Guid.NewGuid(), task, content));
            }

            return BadRequest();
        }
        
        [HttpGet]
        [Route("TaskId/{id:guid}/Comments")]
        public IActionResult GetComments([FromRoute] Guid id)
        {
            ReportTask task = _taskFinder.FindById(id);
            if (task == null) return NotFound($"No task with this id {id}");
            return Ok(_commentsFinder.FindCommentsOfTask(id));
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

                return NotFound($"No task with this id {id}");
            }

            return Ok(_service.GetAll());
        }
    }
}