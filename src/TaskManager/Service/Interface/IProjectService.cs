using TaskManager.Domain.Entities;
using TaskManager.Domain.Response;

namespace TaskManager.Service.Interface
{
    public interface IProjectService
    {
        Task<TaskManagerHttpResponse<IList<Project>>> Get(string userName);
        Task<TaskManagerHttpResponse<bool>> Create(Project project);
        Task<TaskManagerHttpResponse<bool>> Delete(int projectId);
    }
}
