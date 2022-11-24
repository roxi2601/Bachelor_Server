using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.DataAccessLayer.Repositories.Logging;
using Bachelor_Server.Models;
using Quartz;

namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public class Job : IJob
{
    private IServiceProvider _provider;
    private IRestService _restService;
    private IScheduleService _scheduleService;
    private WorkerConfiguration _workerConfiguration;
    private ILogService _logService;

    public Job(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task Execute(IJobExecutionContext context)
    { Console.WriteLine("MA FUT PE MAMA TA");
        using (var scope = _provider.CreateScope())
        {
            _restService = scope.ServiceProvider.GetRequiredService<IRestService>();
            _scheduleService = scope.ServiceProvider.GetRequiredService<IScheduleService>();
            _logService = scope.ServiceProvider.GetRequiredService<ILogService>();
            _workerConfiguration = _scheduleService.GetWorkerConfig();
            
            Console.WriteLine("MA FUT PE MAMA TA");
            string result = "";
            switch (_workerConfiguration.RequestType + _workerConfiguration.LastSavedBody)
            {
                case "getnone": //get with no body

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
                case "deletenone": //delete no body
                    result = await _restService.GenerateDeleteRequest(_workerConfiguration);
                    break;
            }

            Console.WriteLine("IM LOGGING");
            await _logService.Log(result);
        }
    }
}