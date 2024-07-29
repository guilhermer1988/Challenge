using TaskManager.Domain.Entities;
using TaskManager.Domain.Response;

namespace TaskManager.Service.Interface
{
    public interface ITaskHistoryService
    {
        Task<TaskManagerHttpResponse<bool>> InsertHistory(Domain.Entities.Task oldTask, Domain.Entities.Task newTask, Comment? comment);
    }
}
