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
    private IStatisticsRepo _scheduleRepo;
    private IServiceScopeFactory _serviceProvider;

    public ScheduleService(ISchedulerFactory schedulerFactory, IStatisticsRepo scheduleRepo,
        IWorkerConfigurationRepo workerConfigurationRepo, ILogService logService, IServiceScopeFactory serviceProvider)
    {
        _workerConfigurationRepo = workerConfigurationRepo;
        _logService = logService;
        _scheduleRepo = scheduleRepo;
        _schedulerFactory = schedulerFactory;
        _serviceProvider = serviceProvider;
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
                await Scheduler.PauseJob(new JobKey("DEFAULT." + workerConfiguration.Url));
            }
        }
        catch (Exception e)
        {
            await _logService.LogError(e);
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
            // case "sec":
            //     return TriggerBuilder.Create()
            //         .WithIdentity(workerConfiguration.Url + " trigger", workerConfiguration.Url)
            //         .StartAt(new DateTimeOffset((DateTime)workerConfiguration.StartDate))
            //         .WithSimpleSchedule(x => x
            //             .WithIntervalInSeconds(timeValue)
            //             .RepeatForever())
            //         .Build();
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