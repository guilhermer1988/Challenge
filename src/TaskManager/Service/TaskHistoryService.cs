using TaskManager.Domain.Entities;
using TaskManager.Domain.Response;
using TaskManager.Infra.Data.Context;
using TaskManager.Infra.Interface;
using TaskManager.Service.Interface;
using TaskManager.Service.Utils;

namespace TaskManager.Service
{
    public class TaskHistoryService : ITaskHistoryService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskHistoryRepository _taskHistoryRepository;
        public TaskHistoryService(ITaskRepository taskRepository,
            ITaskHistoryRepository taskHistoryRepository)
        {
            _taskRepository = taskRepository;
            _taskHistoryRepository = taskHistoryRepository;
        }

        public async Task<TaskManagerHttpResponse<bool>> InsertHistory(Domain.Entities.Task oldTask, 
            Domain.Entities.Task newTask, 
            Comment? comment)
        {
            var result = new TaskManagerHttpResponse<bool> { Data = new bool() };
            try
            {
                var propsUpdated = CompareObjects.Compare<Domain.Entities.Task>(oldTask, newTask);

                TaskHistory taskHistory = new TaskHistory()
                {
                    TaskId = newTask.Id,
                    Title = newTask.Title,
                    Description = newTask.Description,
                    DueDate = newTask.DueDate,
                    Status = newTask.Status,
                    Details = newTask.Details,
                    Updated = string.Join(", ", propsUpdated.Select(x => x.Key)),
                    UserNameUpdater = newTask.UserName,
                    CommentId = comment?.Id,
                };

                result.Data = await _taskHistoryRepository.InsertAndSave(taskHistory);
            }
            catch (Exception ex)
            {
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                result.ErrorMessage = ex.Message;
                return result;
            }

            result.StatusCode = System.Net.HttpStatusCode.OK;
            return result;
        }
    }
}
