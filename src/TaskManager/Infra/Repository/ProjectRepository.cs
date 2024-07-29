using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infra.Data.Context;
using TaskManager.Infra.Interface;

namespace TaskManager.Infra.Repository
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Project>> GetByUserName(string userName)
        {
            var projects = await _context.Project.
            Select(x => x)
            .Where(x => x.UserName == userName)
            .ToListAsync();

            return projects;
        }
    }
}
