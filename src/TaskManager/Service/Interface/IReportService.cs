using TaskManager.Domain.Entities;
using TaskManager.Domain.Response;

namespace TaskManager.Service.Interface
{
    public interface IReportService
    {
        Task<TaskManagerHttpResponse<List<ReportResponse>>> Get(string userName);
    }
}
