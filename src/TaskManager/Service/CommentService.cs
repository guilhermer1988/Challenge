using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Request;
using TaskManager.Domain.Response;
using TaskManager.Infra.Data.Context;
using TaskManager.Infra.Interface;
using TaskManager.Infra.Repository;
using TaskManager.Service.Interface;

namespace TaskManager.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskService _taskService;
        public CommentService(ICommentRepository commentRepository,
            ITaskRepository taskRepository,
            ITaskService taskService)
        {
            _commentRepository = commentRepository;
            _taskRepository = taskRepository;
            _taskService = taskService;
        }

        public async Task<TaskManagerHttpResponse<bool>> Create(Comment comment)
        {
            var result = new TaskManagerHttpResponse<bool> { Data = new bool() };
            try
            {
                var task = await _taskRepository.GetByTaskId(comment.TaskId);

                if (task == null)
                {
                    result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    result.ErrorMessage = "Task não encontrada";
                    return result;
                }

                var taskRequest = new TaskRequest()
                {
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    Status = task.Status,
                    Details = task.Details,
                    UserName = task.UserName,
                    Id = task.Id,
                };

                await _commentRepository.InsertAndSave(comment);

                await _taskService.Update(taskRequest, comment);
            }
            catch (Exception ex)
            {
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                result.ErrorMessage = ex.Message;
                return result;
            }

            result.StatusCode = System.Net.HttpStatusCode.OK;
            result.Data = true;

            return result;
        }
    }
}
