using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Bachelor_Server.Models;
using Newtonsoft.Json;
using Quartz;
using Quartz.Spi;

namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public class ScheduleService : IScheduleService, IHostedService
{
    private IWorkerConfigurationRepo _workerConfigurationRepo;
    private IRestService _restService;
    private ILogService _logService;
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IJobFactory _jobFactory;
    private readonly IEnumerable<Worker> _workers;

    public IScheduler Scheduler { get; set; }

    public ScheduleService(
        IWorkerConfigurationRepo workerConfigurationRepo,
        IRestService restService,
        ILogService log,
        
        ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IEnumerable<Worker> workers)
    {
       _workerConfigurationRepo = workerConfigurationRepo;
        _restService = restService;
       _logService = log;
        _schedulerFactory = schedulerFactory;
        _jobFactory = jobFactory;
        _workers = workers;
    }

    public async Task CreateWorker(Worker worker)
    {
        WorkerConfiguration workerConfiguration = new WorkerConfiguration();
        try
        {
            workerConfiguration =
                await _workerConfigurationRepo.GetWorkerConfiguration(worker.FkWorkerConfigurationId);
            // //TODO: store worker in DB
            // await _logService.Log("Worker created: " + worker.Name);
           
        }
        catch (Exception e)
        {
            await _logService.LogError(e);
        }

     
    }

    
    private IJobDetail CreateJob(Worker worker)
    {
        Console.WriteLine("IM IN CREATE JOB AT:" + DateTime.Now);
        var type = worker.Type;
        return JobBuilder.Create(type)
               .WithIdentity(type.Name).WithDescription(type.Name).Build();
    }
    private ITrigger CreateTrigger(Worker worker)
    {
        Console.WriteLine("IM IN CREATE Trigger AT:" + DateTime.Now);
        return TriggerBuilder.Create().WithIdentity("BLALALALALAL" + "TRIGGER").WithCronSchedule(worker.ScheduleRate)
            .WithDescription(worker.Name + "TRIGGER").Build();
    }

    private async Task<string> PerformCall(WorkerConfiguration workerConfiguration)
    {
        switch (workerConfiguration.RequestType + workerConfiguration.LastSavedBody)
        {
            case "getnone": //get with no body
                return await _restService.GenerateGetRequest(workerConfiguration);
            case "postform-data": return await _restService.GeneratePostRequestFormData(workerConfiguration);
            case "postraw": return await _restService.GeneratePostRequestRaw(workerConfiguration);
            case "putform-data": return await _restService.GeneratePutRequestFormdata(workerConfiguration);
            case "putraw": return await _restService.GeneratePutRequestRaw(workerConfiguration);
            case "patchform-data": return await _restService.GeneratePatchRequestFormdata(workerConfiguration);
            case "patchraw": return await _restService.GeneratePatchRequestRaw(workerConfiguration);
            case "deletenone": //delete no body
                return await _restService.GenerateDeleteRequest(workerConfiguration);
        }

        return "";
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // try
        // {
        //     await _logService.Log("StartAsync");
        Console.WriteLine("IM IN STARTASYNC AT:" + DateTime.Now);
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            foreach (var VARIABLE in _workers)
            {
                var job = CreateJob(VARIABLE);
                var trigger = CreateTrigger(VARIABLE);
                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }

            await Scheduler.Start(cancellationToken);
        // }
        // catch (Exception e)
        // {
        //     await _logService.LogError(e);
        // }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        // try
        // {
        //     await _logService.Log("StopAsync");
        Console.WriteLine("IM IN STOPASYNC AT:" + DateTime.Now);
            await Scheduler.Shutdown(cancellationToken);
        // }
        // catch (Exception e)
        // {
        //     await _logService.LogError(e);
        // }
    }
}