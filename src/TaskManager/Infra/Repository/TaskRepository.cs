using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infra.Data.Context;
using TaskManager.Infra.Interface;

namespace TaskManager.Infra.Repository
{
    public class TaskRepository : BaseRepository<Domain.Entities.Task>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Domain.Entities.Task> GetById(int taskId)
        {
            var task = await _context.Task.
            Select(x => x)
            .Include(x => x.Project)
            .Include(x => x.Comment)
            .Include(x => x.TaskHistory)
            .Where(x => x.Id == taskId)
            .FirstOrDefaultAsync();

            return task;
        }

        public async Task<List<Domain.Entities.Task>> GetByProject(int projectId)
        {
            var tasks = await _context.Task.
            Select(x => x)
            .Where(x => x.ProjectId == projectId)
            .ToListAsync();

            return tasks;
        }

        public async Task<bool> GetByProjectAndStatus(int projectId, StatusTask status)
        {
            return await _context.Task.
            Select(x => x)
            .AnyAsync(x => x.ProjectId == projectId && x.Status == status);
        }

        public async Task<Domain.Entities.Task> GetByTaskId(int taskId)
        {
            var task = await _context.Task
                .AsNoTracking()
                .Include(x => x.Project)
                .Include(x => x.User)
                .Where(x => x.Id == taskId)
                .FirstOrDefaultAsync();

            return task;
        }
    }
}
