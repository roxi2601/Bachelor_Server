using Bachelor_Server.BusinessLayer.Services.Statistics;
using Bachelor_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Bachelor_Server.BusinessLayer.Controllers
{
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private IStatisticsService _statService;
        public StatisticsController(IStatisticsService statService)
        {
            _statService = statService;
        }

        [HttpGet("getStatisticsForWorkerConfiguration")]
        public async Task<WorkerStatistic> GetStatisticsForWorkerConfiguration()
        {
            var body = new StreamReader(Request.Body).ReadToEndAsync();
            return await
                _statService.GetStatisticsForWorkerConfiguration(JsonConvert.DeserializeObject <int> (body.Result));
        }

        [HttpGet("getAllStatistics")]
        public async Task<List<WorkerStatistic>> GetAllStatistics()
        {
            return await _statService.GetAllStatistics();
        }
    }
}
