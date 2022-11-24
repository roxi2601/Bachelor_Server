using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Bachelor_Server.Models;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public class ScheduleService : IScheduleService, IJob

    // , IHostedService, IJob
{
    private IWorkerConfigurationRepo _workerConfigurationRepo;
    private IRestService _restService;
    private ILogService _logService;
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IJobFactory _jobFactory;
    private readonly IEnumerable<Worker> _workers;
    private readonly IServiceScopeFactory _scopeFactory;
    private IScheduler Scheduler;
    private WorkerConfiguration _workerConfiguration { get; set; }

    public ScheduleService(
        ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IEnumerable<Worker> workers,
        IServiceScopeFactory scopeFactory
    )
    {
        _scopeFactory = scopeFactory;
        _workerConfigurationRepo =
            _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IWorkerConfigurationRepo>();
        _restService = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IRestService>();
        _logService = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ILogService>();
        _schedulerFactory = schedulerFactory;
        _jobFactory = jobFactory;
        _workers = workers;
    }

    public async Task CreateWorker(Worker worker1)
    {
        try
        {
            // _workerConfiguration =
            //     await _workerConfigurationRepo.GetWorkerConfiguration(worker.FkWorkerConfigurationId);
            //TODO: store worker in DB

            Worker worker = new Worker();
            
            var job = CreateJob(worker);
            var trigger = CreateTrigger(worker);

            Scheduler = await _schedulerFactory.GetScheduler();
            await Scheduler.Start();
            await Scheduler.ScheduleJob(job, trigger);

            await _logService.Log("Worker created: " + this.GetType().ToString());
        }
        catch (Exception e)
        {
            await _logService.LogError(e);
        }
    }

    private IJobDetail CreateJob(Worker worker)
    {
        return JobBuilder.Create(worker.GetType())
            .WithIdentity(worker.GetType().ToString()).WithDescription(worker.GetType().ToString()).Build();
    }

    private ITrigger CreateTrigger(Worker worker)
    {
        return TriggerBuilder.Create().WithIdentity(worker.GetType().ToString())
            .WithDescription(worker.GetType() + " TRIGGER").WithSimpleSchedule(x =>
            {
                x.WithIntervalInSeconds(30).RepeatForever();
            }).Build();
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("MA FUT PE MAMA TA");
        string result = "";
        switch (_workerConfiguration.RequestType + _workerConfiguration.LastSavedBody)
        {
            case "getnone": //get with no body

                result = await _restService.GenerateGetRequest(_workerConfiguration);
                break;
            case "postform-data": result = await _restService.GeneratePostRequestFormData(_workerConfiguration);
                break;
            case "postraw": result =  await _restService.GeneratePostRequestRaw(_workerConfiguration); break;
                
            case "putform-data": result =  await _restService.GeneratePutRequestFormdata(_workerConfiguration); break;
            case "putraw": result = await _restService.GeneratePutRequestRaw(_workerConfiguration);
                break;
            case "patchform-data": result = await _restService.GeneratePatchRequestFormdata(_workerConfiguration);
                break;
            case "patchraw": result =  await _restService.GeneratePatchRequestRaw(_workerConfiguration);
                break;
            case "deletenone": //delete no body
                 result = await _restService.GenerateDeleteRequest(_workerConfiguration);
                break;
        }
        Console.WriteLine("IM LOGGING");
        await _logService.Log(result);
    }
}