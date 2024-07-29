using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Dto;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Request;
using TaskManager.Infra.Interface;
using TaskManager.Service;
using TaskManager.Service.Interface;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {

        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet(Name = "GetReport")]
        public async Task<IActionResult> GetReport(string userName)
        {
            var result = await _reportService.Get(userName);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

    }
}
