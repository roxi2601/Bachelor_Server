using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.Models.WorkerConfiguration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bachelor_Server.BusinessLayer.Controllers;

[ApiController]
public class WorkerConfigsController : ControllerBase
{
    private ILogHandling _logHandling;
    private IWorkerConfigService _workerConfigService;

    public WorkerConfigsController(ILogHandling logService, IWorkerConfigService workerConfigService)
    {
        _logHandling = logService;
        _workerConfigService = workerConfigService;
    }
    
    [HttpGet("workerConfig")]
    public async Task<List<WorkerConfigurationModel>> ReadAllWorkerConfigurations()
    {
        try
        {
            return await _workerConfigService.ReadAllWorkerConfigurations();
        }
        catch (Exception e)
        {
            await _logHandling.Log(e);
        }

        return new List<WorkerConfigurationModel>();
    }
    
    [HttpPost("workerConfig")]
    public async Task CreateWorkerConfiguration()
    {
        try
        {
            var reader = new StreamReader(Request.Body);
            reader.BaseStream.Seek(0, SeekOrigin.Begin); 
            var rawMessage = await reader.ReadToEndAsync();
            await _workerConfigService.CreateWorkerConfiguration(
                JsonConvert.DeserializeObject<WorkerConfigurationModel>(rawMessage));
        }
        catch (Exception e)
        {
            await _logHandling.Log(e);
        }
       
    }
    
    [HttpPatch("workerConfig")]
    public async Task EditWorkerConfiguration()
    {
        try
        {
            var reader = new StreamReader(Request.Body);
            reader.BaseStream.Seek(0, SeekOrigin.Begin); 
            var rawMessage = await reader.ReadToEndAsync();
            await _workerConfigService.EditWorkerConfiguration(
                JsonConvert.DeserializeObject<WorkerConfigurationModel>(rawMessage));
        }
        catch (Exception e)
        {
            await _logHandling.Log(e);
        }
       
    }
    
    [HttpDelete("workerConfig/{id}")]
    public async Task DeleteWorkerConfiguration(int id)
    {
        try
        {
            await _workerConfigService.DeleteWorkerConfiguration(id);
        }
        catch(Exception e)
        {
            await _logHandling.Log(e);
        }
    }
}