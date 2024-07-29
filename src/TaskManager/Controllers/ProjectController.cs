using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Request;
using TaskManager.Service;
using TaskManager.Service.Interface;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet(Name = "GetAllProjectsByUser")]
        public async Task<IActionResult> GetByUser(string userName)
        {
            var result = await _projectService.Get(userName);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpPost(Name = "CreateProject")]
        public async Task<IActionResult> Post([FromBody] ProjectRequest projectRequest)
        {
            Project project = new Project()
            {
                Title = projectRequest.Title,
                Description = projectRequest.Description,
                UserName = projectRequest.UserName,
            };

            var result = await _projectService.Create(project);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpDelete(Name = "DeleteProject")]
        public async Task<IActionResult> Delete(int projectId)
        {
            var result = await _projectService.Delete(projectId);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

    }
}
