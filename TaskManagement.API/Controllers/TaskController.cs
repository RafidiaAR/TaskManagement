using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Domain.Task.DTO;
using TaskManagement.API.Domain.Task.Entities;
using TaskManagement.API.Domain.Task.Repositories;
using TaskManagement.API.Domain.User.Dto;
using TaskManagement.API.Domain.User.Entities;
using TaskManagement.API.Domain.User.Repositories;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskRepository _taskRepository;

        public TaskController(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(CreateTaskDTO request)
        {
            var data = new TaskEntity
            {
                Title = request.Title,
                Description = request.Description,
                Assignee = request.Assignee,
                Duedate = request.DueDate,
                Priority = request.Priority,
                Status = request.Status,
                CreatedBy = request.CreatedBy
            };
            _taskRepository.Create(data);

            var result = new
            {
                IsSuccess = true
            };

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update(UpdateTaskDTO request)
        {
            var data = new TaskEntity
            {
                TaskId = request.TaskID,
                Title = request.Title,
                Description = request.Description,
                Assignee = request.Assignee,
                Duedate = request.DueDate,
                Priority = request.Priority,
                Status = request.Status,
                Progress = request.Progress,
                UpdatedBy = request.UpdatedBy
            };
            _taskRepository.Update(data);

            var result = new
            {
                IsSuccess = true
            };

            return Ok(result);
        }


        [HttpGet]
        [Route("FindAll")]
        public IActionResult FindAll() 
        {
            var tasks = _taskRepository.FindAll();

            var result = tasks.Select(x => new GetTaskDTO
            {
                TaskId = x.TaskId,
                Title = x.Title,
                Description = x.Description,
                Assignee = x.Assignee,
                Duedate = x.Duedate,
                Priority = x.Priority,
                Status = x.Status,
                Progress = x.Progress
            }).ToList();

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult FindAll(Guid id)
        {
            _taskRepository.Delete(id);
            var result = new
            {
                IsSuccess = true
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("FindById/{id}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            var tasks = await _taskRepository.FindById(id);

            var result = new GetTaskDTO
            {
                TaskId = tasks.TaskId,
                Title = tasks.Title,
                Description = tasks.Description,
                Assignee = tasks.Assignee,
                Duedate = tasks.Duedate,
                Priority = tasks.Priority,
                Status = tasks.Status,
                Progress = tasks.Progress
            };
            return Ok(result);
        }
    }
}
