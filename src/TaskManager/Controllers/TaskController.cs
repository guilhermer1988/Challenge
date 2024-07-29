using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Dto;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Request;
using TaskManager.Infra.Interface;
using TaskManager.Service;
using TaskManager.Service.Interface;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {

        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet(Name = "GetAllTasksByProjects")]
        public async Task<IActionResult> GetByProjects(int projectId)
        {
            var result = await _taskService.Get(projectId);
            return Ok(result);
        }

        [HttpPost(Name = "CreateTask")]
        public async Task<IActionResult> Post([FromBody] TaskRequest taskRequest)
        {
            Domain.Entities.Task task = new Domain.Entities.Task()
            {
                Title = taskRequest.Title,
                Description = taskRequest.Description,
                DueDate = taskRequest.DueDate,
                Priority = taskRequest.Priority,
                Status = taskRequest.Status,
                Details = taskRequest.Details,
                ProjectId = taskRequest.ProjectId,
                UserName = taskRequest.UserName,
            };

            var result = await _taskService.Create(task);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpPut(Name = "UpdateTask")]
        public async Task<IActionResult> Update([FromBody] TaskRequest taskRequest)
        {
            var result = await _taskService.Update(taskRequest);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpDelete(Name = "DeleteTask")]
        public async Task<IActionResult> Delete(int taskId)
        {
            var result = await _taskService.Delete(taskId);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

    }
}
