using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infra.Data.Context;
using TaskManager.Infra.Interface;

namespace TaskManager.Infra.Repository
{
    public class TaskHistoryRepository : BaseRepository<TaskHistory>, ITaskHistoryRepository
    {
        public TaskHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
