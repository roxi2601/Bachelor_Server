﻿using Bachelor_Server.BusinessLayer.Services.Logging;
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
        var body = new StreamReader(Request.Body).ReadToEndAsync();
        await _workerConfigService.CreateWorkerConfiguration(
            JsonConvert.DeserializeObject<WorkerConfigurationModel>(body.Result));
    }

    [HttpPatch("workerConfig")]
    public async Task EditWorkerConfiguration()
    {
        var body = new StreamReader(Request.Body).ReadToEndAsync();
        await _workerConfigService.EditWorkerConfiguration(
            JsonConvert.DeserializeObject<WorkerConfigurationModel>(body.Result));
    }

    [HttpDelete("workerConfig/{id}")]
    public async Task DeleteWorkerConfiguration(int id)
    {
        await _workerConfigService.DeleteWorkerConfiguration(id);
    }
}