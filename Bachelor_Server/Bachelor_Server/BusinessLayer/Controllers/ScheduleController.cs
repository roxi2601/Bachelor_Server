using Bachelor_Server.BusinessLayer.Services.ScheduleService;
using Bachelor_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bachelor_Server.BusinessLayer.Controllers;

public class ScheduleController : ControllerBase
{
    public IScheduleService _ScheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _ScheduleService = scheduleService;
    }
    
    
    [HttpPost("scheduleWorker")]
    public async Task CreateWorker()
    {
        var body = new StreamReader(Request.Body).ReadToEndAsync();
        await _ScheduleService.ScheduleWorkerConfiguration(
            JsonConvert.DeserializeObject<WorkerConfiguration>(body.Result));
    }
}