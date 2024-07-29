using TaskManager.Domain.Entities;
using TaskManager.Domain.Response;

namespace TaskManager.Service.Interface
{
    public interface ICommentService
    {
        Task<TaskManagerHttpResponse<bool>> Create(Comment comment);
    }
}
