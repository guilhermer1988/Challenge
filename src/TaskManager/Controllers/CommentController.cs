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
    public class CommentController : ControllerBase
    {

        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost(Name = "CreateComment")]
        public async Task<IActionResult> Post([FromBody] CommentRequest commentRequest)
        {
            Comment comment = new Comment()
            {
                Description = commentRequest.Description,
                UserName = commentRequest.UserName,
                TaskId = commentRequest.TaskId,
            };

            var result = await _commentService.Create(comment);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}
