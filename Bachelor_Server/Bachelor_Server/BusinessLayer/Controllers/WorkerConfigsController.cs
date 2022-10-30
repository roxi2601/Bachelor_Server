using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.Models.Authorization;
using Bachelor_Server.Models.Body;
using Bachelor_Server.Models.WorkerConfiguration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bachelor_Server.BusinessLayer.Controllers;

[ApiController]
public class WorkerConfigsController : ControllerBase
{
    private IWorkerConfigService _workerConfigService;

    public WorkerConfigsController(IWorkerConfigService workerConfigService)
    {
        _workerConfigService = workerConfigService;
    }

    [HttpGet("workerConfig")]
    public async Task<List<WorkerConfigurationModel>> ReadAllWorkerConfigurations()
    {
        return await _workerConfigService.ReadAllWorkerConfigurations();
    }

    [HttpPost("workerConfig")]
    public async Task CreateWorkerConfiguration()
    {
        var reader = new StreamReader(Request.Body);
        reader.BaseStream.Seek(0, SeekOrigin.Begin);
        var rawMessage = await reader.ReadToEndAsync();
        await _workerConfigService.CreateWorkerConfiguration(
            JsonConvert.DeserializeObject<WorkerConfigurationModel>(rawMessage));
    }

    [HttpPatch("workerConfig")]
    public async Task EditWorkerConfiguration()
    {
        var reader = new StreamReader(Request.Body);
        reader.BaseStream.Seek(0, SeekOrigin.Begin);
        var rawMessage = await reader.ReadToEndAsync();
        await _workerConfigService.EditWorkerConfiguration(
            JsonConvert.DeserializeObject<WorkerConfigurationModel>(rawMessage));
    }

    [HttpDelete("workerConfig/{id}")]
    public async Task DeleteWorkerConfiguration(int id)
    {
        await _workerConfigService.DeleteWorkerConfiguration(id);
    }
}