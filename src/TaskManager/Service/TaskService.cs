using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Request;
using TaskManager.Domain.Response;
using TaskManager.Infra.Interface;
using TaskManager.Service.Interface;
using TaskManager.Service.Utils;

namespace TaskManager.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskHistoryService _taskHistoryService;
        public TaskService(ITaskRepository taskRepository,
            ITaskHistoryService taskHistoryService)
        {
            _taskRepository = taskRepository;
            _taskHistoryService = taskHistoryService;
        }

        public async Task<IList<Domain.Entities.Task>> Get(int projectId)
        {
            return await _taskRepository.GetByProject(projectId);
        }

        public async Task<TaskManagerHttpResponse<bool>> Create(Domain.Entities.Task task)
        {
            var result = new TaskManagerHttpResponse<bool> { Data = new bool() };
            try
            {
                if (await HaveMoreThan20Tasks(task.ProjectId))
                {
                    result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    result.ErrorMessage = "Erro ao adicionar Task. Cada projeto tem um limite máximo de 20 tarefas e este limite foi atingido.";
                    return result;
                }

                result.Data = await _taskRepository.InsertAndSave(task);
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

        private async Task<bool> HaveMoreThan20Tasks(int projectId)
        {
            var tasks = await Get(projectId);
            return tasks != null && tasks.Count > 20;
        }

        public async Task<TaskManagerHttpResponse<bool>> Update(TaskRequest taskRequest, Comment? comment)
        {
            var result = new TaskManagerHttpResponse<bool> { Data = new bool() };
            try
            {
                var oldTask = await _taskRepository.GetByTaskId(taskRequest.Id);

                var newTask = new Domain.Entities.Task()
                {
                    Id = oldTask.Id,
                    Title = taskRequest.Title,
                    Description = taskRequest.Description,
                    DueDate = taskRequest.DueDate,
                    Status = taskRequest.Status,
                    Details = taskRequest.Details,
                    UserName = taskRequest.UserName,
                    ProjectId = oldTask.ProjectId,
                };

                var resultHistory = await _taskHistoryService.InsertHistory(oldTask, newTask, comment);

                if (resultHistory.StatusCode != System.Net.HttpStatusCode.OK) 
                {
                    result.StatusCode = resultHistory.StatusCode;
                    result.ErrorMessage = resultHistory.ErrorMessage;
                    return result;
                }

                result.Data = await _taskRepository.UpdateAndSave(newTask);

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

        public async Task<TaskManagerHttpResponse<bool>> Delete(int taskId)
        {
            var result = new TaskManagerHttpResponse<bool> { Data = new bool() };
            try
            {
                result.Data = await _taskRepository.Delete(taskId);
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

        public async Task<bool> GetByProjectAndStatus(int projectId, StatusTask status)
        {
            return await _taskRepository.GetByProjectAndStatus(projectId, status);
        }

        public async Task<Domain.Entities.Task> GetById(int taskId)
        {
            return await _taskRepository.GetById(taskId);
        }
    }
}
