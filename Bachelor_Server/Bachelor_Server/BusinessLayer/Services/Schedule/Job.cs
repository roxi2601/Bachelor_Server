using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.Models;
using Quartz;

namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public class Job : IJob
{
    private IServiceScopeFactory  _serviceProvider;
    public Job(IServiceScopeFactory  provider)
    {
        _serviceProvider = provider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            IRestService _restService = scope.ServiceProvider.GetRequiredService<IRestService>();
            IScheduleService _scheduleService = scope.ServiceProvider.GetRequiredService<IScheduleService>();
            ILogService _logService = scope.ServiceProvider.GetRequiredService<ILogService>();
            WorkerConfiguration _workerConfiguration = (WorkerConfiguration)context.JobDetail.JobDataMap.Get("workerConfiguration");


            string result = "";
            switch (_workerConfiguration.RequestType + _workerConfiguration.LastSavedBody)
            {
                case "getnone":

                    result = await _restService.GenerateGetRequest(_workerConfiguration);
                    break;
                case "postform-data":
                    result = await _restService.GeneratePostRequestFormData(_workerConfiguration);
                    break;
                case "postraw":
                    result = await _restService.GeneratePostRequestRaw(_workerConfiguration);
                    break;

                case "putform-data":
                    result = await _restService.GeneratePutRequestFormdata(_workerConfiguration);
                    break;
                case "putraw":
                    result = await _restService.GeneratePutRequestRaw(_workerConfiguration);
                    break;
                case "patchform-data":
                    result = await _restService.GeneratePatchRequestFormdata(_workerConfiguration);
                    break;
                case "patchraw":
                    result = await _restService.GeneratePatchRequestRaw(_workerConfiguration);
                    break;
                case "deletenone": 
                    result = await _restService.GenerateDeleteRequest(_workerConfiguration);
                    break;
            }

            await _logService.Log(result);
        }
    }
}