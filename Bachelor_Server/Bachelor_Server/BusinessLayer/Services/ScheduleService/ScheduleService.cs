using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.DataAccessLayer.Repositories.Schedule;
using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Bachelor_Server.Models;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public class ScheduleService : IScheduleService
{
    private IWorkerConfigurationRepo _workerConfigurationRepo;
    private ILogService _logService;
    private readonly ISchedulerFactory _schedulerFactory;
    private IScheduler Scheduler;
    private WorkerConfiguration _workerConfiguration;
    private IScheduleRepo _scheduleRepo;
    private IServiceProvider _serviceProvider;

    public ScheduleService(ISchedulerFactory schedulerFactory, IScheduleRepo scheduleRepo,
        IWorkerConfigurationRepo workerConfigurationRepo, ILogService logService, IServiceProvider serviceProvider)
    {
        _workerConfigurationRepo = workerConfigurationRepo;
        _logService = logService;
        _scheduleRepo = scheduleRepo;
        _schedulerFactory = schedulerFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task CreateWorker(Worker worker)
    {
        try
        {
            await _scheduleRepo.CreateWorker(worker);
            _workerConfiguration =
                await _workerConfigurationRepo.GetWorkerConfiguration(worker.FkWorkerConfigurationId);

         //   Job job1 = new Job(_serviceProvider);
            
            // var job = JobBuilder
            //     .Create(job1.GetType())
            //     .WithIdentity(job1.GetType() + "")
            //     .WithDescription(job1.GetType() + "")
            //     .Build();
            //
            // var trigger = TriggerBuilder
            //     .Create()
            //     .WithIdentity(job1.GetType() + ".trigger")
            //     .WithSimpleSchedule(x => x
            //                 .WithIntervalInSeconds(30)
            //                  .RepeatForever())
            //     .WithDescription(job1.GetType() + ".trigger")
            //     .Build();

            var job = JobBuilder.Create<Job>()
                .WithIdentity("myJob", "group1")
                .Build();
            
            var trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();

            Scheduler = await _schedulerFactory.GetScheduler();
            Scheduler.JobFactory = new SingletonJobFactory(_serviceProvider);
            await Scheduler.Start();
            await Scheduler.ScheduleJob(job, trigger);

            await _logService.Log("Worker created: " + this.GetType().ToString());
        }
        catch (Exception e)
        {
            await _logService.LogError(e);
        }
    }

    public WorkerConfiguration GetWorkerConfig()
    {
        return _workerConfiguration;
    }

    // private IJobDetail CreateJob(Job job)
    // {
    //     return JobBuilder.Create(job.GetType())
    //         .WithIdentity(job.GetType().ToString()).WithDescription(job.GetType().ToString()).Build();
    // }
    //
    // private ITrigger CreateTrigger(Job job)
    // {
    //     return TriggerBuilder.Create().WithIdentity(job.GetType().ToString())
    //         .WithDescription(job.GetType() + " TRIGGER").WithSimpleSchedule(x =>
    //         {
    //             x.WithIntervalInSeconds(30).RepeatForever();
    //         }).Build();
    // }

    // public async Task Execute(IJobExecutionContext context)
    // {
    //     Console.WriteLine("MA FUT PE MAMA TA");
    //     string result = "";
    //     switch (_workerConfiguration.RequestType + _workerConfiguration.LastSavedBody)
    //     {
    //         case "getnone": //get with no body
    //
    //             result = await _restService.GenerateGetRequest(_workerConfiguration);
    //             break;
    //         case "postform-data": result = await _restService.GeneratePostRequestFormData(_workerConfiguration);
    //             break;
    //         case "postraw": result =  await _restService.GeneratePostRequestRaw(_workerConfiguration); break;
    //             
    //         case "putform-data": result =  await _restService.GeneratePutRequestFormdata(_workerConfiguration); break;
    //         case "putraw": result = await _restService.GeneratePutRequestRaw(_workerConfiguration);
    //             break;
    //         case "patchform-data": result = await _restService.GeneratePatchRequestFormdata(_workerConfiguration);
    //             break;
    //         case "patchraw": result =  await _restService.GeneratePatchRequestRaw(_workerConfiguration);
    //             break;
    //         case "deletenone": //delete no body
    //              result = await _restService.GenerateDeleteRequest(_workerConfiguration);
    //             break;
    //     }
    //     Console.WriteLine("IM LOGGING");
    //     await _logService.Log(result);
    // }
}