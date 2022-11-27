using Bachelor_Server.BusinessLayer.Services.Account;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.BusinessLayer.Services.Statistics;
using Bachelor_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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

        [HttpPost("createStatistic")]
        public async Task CreateStatistics()
        {
            var body = new StreamReader(Request.Body).ReadToEndAsync();
            await _statService.CreateStatistics(JsonConvert.DeserializeObject<WorkerStatistic>(body.Result));
        }

        [HttpGet("getStatisticsForWorkerConfiguration")]
        public async Task<List<WorkerStatistic>> GetStatisticsForWorkerConfiguration()
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
