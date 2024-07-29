using TaskManager.Domain.Entities;
using TaskManager.Domain.Request;
using TaskManager.Domain.Response;

namespace TaskManager.Service.Interface
{
    public interface ITaskService
    {
        Task<Domain.Entities.Task> GetById(int taskId);
        Task<IList<Domain.Entities.Task>> Get(int projectId);
        Task<TaskManagerHttpResponse<bool>> Create(Domain.Entities.Task task);
        Task<TaskManagerHttpResponse<bool>> Update(TaskRequest task, Comment? comment = null);
        Task<TaskManagerHttpResponse<bool>> Delete(int taskId);
        Task<bool> GetByProjectAndStatus(int projectId, StatusTask status);
    }
}
