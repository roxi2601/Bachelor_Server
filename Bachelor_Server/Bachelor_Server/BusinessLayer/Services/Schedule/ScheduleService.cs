using Bachelor_Server.BusinessLayer.Services.Email;
using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Bachelor_Server.Models;
using Quartz;


namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public class ScheduleService : IScheduleService
{
    private IWorkerConfigurationRepo _workerConfigurationRepo;
    private ILogService _logService;
    private readonly ISchedulerFactory _schedulerFactory;
    private IScheduler Scheduler;
    private IServiceScopeFactory _serviceProvider;
    private IEmailSerivce _emailSerivce;

    public ScheduleService(ISchedulerFactory schedulerFactory,
        IWorkerConfigurationRepo workerConfigurationRepo, ILogService logService, IServiceScopeFactory serviceProvider,
        IEmailSerivce emailSerivce)
    {
        _workerConfigurationRepo = workerConfigurationRepo;
        _logService = logService;
        _schedulerFactory = schedulerFactory;
        _serviceProvider = serviceProvider;
        _emailSerivce = emailSerivce;
    }

    public async Task ScheduleWorkerConfiguration(WorkerConfiguration workerConfiguration)
    {
        try
        {
            Scheduler = await _schedulerFactory.GetScheduler();
            Scheduler.JobFactory = new SingletonJobFactory(_serviceProvider);

            await _workerConfigurationRepo.EditSchedule(workerConfiguration);

            if (workerConfiguration.IsActive)
            {
                var jobKey = new JobKey(workerConfiguration.Url);
                if (await Scheduler.CheckExists(jobKey))
                {
                    await _logService.Log(jobKey + " already exists");
                    return;
                }

                var job = JobBuilder.Create<Job>()
                    .WithIdentity(workerConfiguration.Url)
                    .Build();

                job.JobDataMap.Add("workerConfiguration", workerConfiguration);

                var trigger = BuildTrigger(workerConfiguration);

                await Scheduler.Start();

                await Scheduler.ScheduleJob(job, trigger);

                await _logService.Log("Worker created: " + workerConfiguration.Url);
            }
            else
            {
                await Scheduler.UnscheduleJob(new TriggerKey(workerConfiguration.Url + " trigger"));
            }
        }
        catch (Exception e)
        {
            string response = await _logService.LogError(e);
            _emailSerivce.SendEmailAboutError("SCHEDULING ERROR!!" + "\n\n\n" + response);
        }
    }

    public async Task RunAllSchedules()
    {
        List<WorkerConfiguration> schedules = await _workerConfigurationRepo.GetWorkerConfigurations();
        foreach (var VARIABLE in schedules)
        {
            await ScheduleWorkerConfiguration(VARIABLE);
        }
    }


    private ITrigger BuildTrigger(WorkerConfiguration workerConfiguration)
    {
        int timeValue =
            Int32.Parse(workerConfiguration.ScheduleRate.Substring(0, workerConfiguration.ScheduleRate.IndexOf('/')));
        string timeType =
            workerConfiguration.ScheduleRate.Substring(workerConfiguration.ScheduleRate.IndexOf('/') + 1);

        switch (timeType)
        {
            case "min":
                return TriggerBuilder.Create()
                    .WithIdentity(workerConfiguration.Url + " trigger")
                    .StartAt(new DateTimeOffset((DateTime)workerConfiguration.StartDate))
                    .WithSimpleSchedule(x => x
                        .WithIntervalInMinutes(timeValue)
                        .RepeatForever())
                    .Build();
            case "h":
                return TriggerBuilder.Create()
                    .WithIdentity(workerConfiguration.Url + " trigger")
                    .StartAt(new DateTimeOffset((DateTime)workerConfiguration.StartDate))
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(timeValue)
                        .RepeatForever())
                    .Build();
        }

        return null;
    }
}