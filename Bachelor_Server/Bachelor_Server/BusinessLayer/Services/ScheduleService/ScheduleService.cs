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
        WorkerConfiguration workerConfiguration = new WorkerConfiguration();
        try
        {
            // workerConfiguration =
            //     await _workerConfigurationRepo.GetWorkerConfiguration(worker.FkWorkerConfigurationId);
            //TODO: store worker in DB

            Worker worker = new Worker();
            worker.Name = "TESTING";
            worker.ScheduleRate = "TESTING";
            
            var job = CreateJob(worker);
            var trigger = CreateTrigger(worker);

            Scheduler = await _schedulerFactory.GetScheduler();
            await Scheduler.Start();
            await Scheduler.ScheduleJob(job, trigger);

            await _logService.Log("Worker created: " + worker.Name);
        }
        catch (Exception e)
        {
            await _logService.LogError(e);
        }
    }


    private IJobDetail CreateJob(Worker worker)
    {
        return JobBuilder.Create(worker.GetType())
            .WithIdentity(worker.Name).WithDescription(worker.Name).Build();
    }

    private ITrigger CreateTrigger(Worker worker)
    {
        return TriggerBuilder.Create().WithIdentity(worker.Name)
            .WithDescription(worker.Name + " TRIGGER").WithSimpleSchedule(x =>
            {
                x.WithIntervalInSeconds(30).RepeatForever();
            }).Build();
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

    // public async Task StartAsync(CancellationToken cancellationToken)
    // {
    //     // try
    //     // {
    //     //     await _logService.Log("StartAsync");
    //     Console.WriteLine("IM IN STARTASYNC AT:" + DateTime.Now);
    //
    //
    //     Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
    //     Scheduler.JobFactory = _jobFactory;
    //     foreach (var VARIABLE in _workers)
    //     {
    //         var job = CreateJob(VARIABLE);
    //         var trigger = CreateTrigger(VARIABLE);
    //         await Scheduler.ScheduleJob(job, trigger, cancellationToken);
    //     }
    //
    //     await Scheduler.Start(cancellationToken);
    //     // }
    //     // catch (Exception e)
    //     // {
    //     //     await _logService.LogError(e);
    //     // }
    // }
    //
    // public async Task StopAsync(CancellationToken cancellationToken)
    // {
    //     // try
    //     // {
    //     //     await _logService.Log("StopAsync");
    //     Console.WriteLine("IM IN STOPASYNC AT:" + DateTime.Now);
    //     await Scheduler.Shutdown(cancellationToken);
    //     // }
    //     // catch (Exception e)
    //     // {
    //     //     await _logService.LogError(e);
    //     // }
    // }
    //
    // public Task Execute(IJobExecutionContext context)
    // {
    //     throw new NotImplementedException();
    // }
    public Task Execute(IJobExecutionContext context)
    {
        throw new NotImplementedException();
    }
}