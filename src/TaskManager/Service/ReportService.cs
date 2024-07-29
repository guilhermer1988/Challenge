using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Response;
using TaskManager.Infra.Data.Context;
using TaskManager.Infra.Interface;
using TaskManager.Infra.Repository;
using TaskManager.Service.Interface;
using TaskManager.Service.Utils;

namespace TaskManager.Service
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskManagerHttpResponse<List<ReportResponse>>> Get(string userName)
        {
            var result = new TaskManagerHttpResponse<List<ReportResponse>>
            {
                Data = new List<ReportResponse>()
            };

            try
            {
                var user = await _context.User
                    .Where(x => x.UserName == userName &&
                    x.Type == TypeUser.Manager)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    var limitDate = DateTime.UtcNow.AddDays(-30);

                    var tasks = await _context.Task
                        .Include(x => x.Project)
                        .Where(t => t.Status == StatusTask.Done &&
                                    t.UpdateAt.Date >= limitDate.Date)
                        .ToListAsync();

                    var report = _context.User
                        .Select(u => new ReportResponse
                        {
                            UserName = u.UserName,
                            Name = u.Name,
                            UserEmail = u.Email,
                            AverageTaskDone = CalcAverange(u, tasks),
                        }).ToList();

                    result.Data = report;
                }
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

        private static double CalcAverange(User u, List<Domain.Entities.Task> tasks)
        {
            var averange = tasks
                .Where(t => t.Project.UserName == u.UserName)
                .Count() / 30.0;

            return averange;
        }
    }
}
