using TaskManager.Domain.Entities;

namespace TaskManager.Infra.Interface
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<List<Project>> GetByUserName(string userName);
    }
}
