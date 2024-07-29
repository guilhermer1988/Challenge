using TaskManager.Domain.Entities;

namespace TaskManager.Infra.Interface
{
    public interface ITaskRepository : IRepository<Domain.Entities.Task>
    {
        Task<Domain.Entities.Task> GetById(int taskId);
        Task<List<Domain.Entities.Task>> GetByProject(int projectId);
        Task<bool> GetByProjectAndStatus(int projectId, StatusTask status);
        Task<Domain.Entities.Task> GetByTaskId(int taskId);
    }
}
