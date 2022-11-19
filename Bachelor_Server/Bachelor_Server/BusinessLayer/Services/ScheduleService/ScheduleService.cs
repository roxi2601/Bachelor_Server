using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Bachelor_Server.Models;
using Newtonsoft.Json;

namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public class ScheduleService : IScheduleService
{
    private IWorkerConfigurationRepo _workerConfigurationRepo;
    private IRestService _restService;
    private ILogService _logService;

    public WorkerConfiguration GlobalWorkerConfiguration { get; set; }

    public ScheduleService(IWorkerConfigurationRepo workerConfigurationRepo, IRestService restService, ILogService log)
    {
        _workerConfigurationRepo = workerConfigurationRepo;
        _restService = restService;
        _logService = log;
    }

    public async Task CreateWorker(Worker worker)
    {
        GlobalWorkerConfiguration =
            await _workerConfigurationRepo.GetWorkerConfiguration(worker.FkWorkerConfigurationId);
        
    }

    public async Task<string> PerformCall()
    {
        switch (GlobalWorkerConfiguration.RequestType + GlobalWorkerConfiguration.LastSavedBody)
        {
            case "getnone": //get with no body
                return await _restService.GenerateGetRequest(GlobalWorkerConfiguration);
            case "postform-data": return await _restService.GeneratePostRequestFormData(GlobalWorkerConfiguration);
            case "postraw": return await _restService.GeneratePostRequestRaw(GlobalWorkerConfiguration);
            case "putform-data": return await _restService.GeneratePutRequestFormdata(GlobalWorkerConfiguration);
            case "putraw": return await _restService.GeneratePutRequestRaw(GlobalWorkerConfiguration);
            case "patchform-data": return await _restService.GeneratePatchRequestFormdata(GlobalWorkerConfiguration);
            case "patchraw": return await _restService.GeneratePatchRequestRaw(GlobalWorkerConfiguration);
            case "deletenone": //delete no body
                return await _restService.GenerateDeleteRequest(GlobalWorkerConfiguration);
        }

        return "";
    }
}