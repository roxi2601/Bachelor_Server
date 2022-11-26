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


    private WorkerConfiguration _workerConfiguration;

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
            if (workerConfiguration.IsActive)
            {
                await _workerConfigurationRepo.EditSchedule(workerConfiguration);

                var job = JobBuilder.Create<Job>()
                    .WithIdentity(workerConfiguration.Url, workerConfiguration.Url)
                    .Build();

                job.JobDataMap.Add("workerConfiguration", workerConfiguration);

                var trigger = BuildTrigger(workerConfiguration);

                Scheduler = await _schedulerFactory.GetScheduler();
                Scheduler.JobFactory = new SingletonJobFactory(_serviceProvider);
                await Scheduler.Start();
                await Scheduler.ScheduleJob(job, trigger);

                await _logService.Log("Worker created: " + job.GetType());
            }
        }
        catch (Exception e)
        {
            await _logService.LogError(e);
        }
    }


    private ITrigger BuildTrigger(WorkerConfiguration workerConfiguration)
    {
        int timeValue =
            Int32.Parse(workerConfiguration.ScheduleRate.Substring(0, workerConfiguration.ScheduleRate.IndexOf('/')));
        string timeType =
            workerConfiguration.ScheduleRate.Substring(_workerConfiguration.ScheduleRate.LastIndexOf('/') + 1);

        switch (timeType)
        {
            case "seconds":
                return TriggerBuilder.Create()
                    .WithIdentity(workerConfiguration.Url + " trigger", workerConfiguration.Url)
                    .StartAt(new DateTimeOffset((DateTime)workerConfiguration.StartDate))
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(timeValue)
                        .RepeatForever())
                    .Build();
            case "minutes":
                return TriggerBuilder.Create()
                    .WithIdentity(workerConfiguration.Url + " trigger", workerConfiguration.Url)
                    .StartAt(new DateTimeOffset((DateTime)workerConfiguration.StartDate))
                    .WithSimpleSchedule(x => x
                        .WithIntervalInMinutes(timeValue)
                        .RepeatForever())
                    .Build();
            case "hours":
                return TriggerBuilder.Create()
                    .WithIdentity(workerConfiguration.Url + " trigger", workerConfiguration.Url)
                    .StartAt(new DateTimeOffset((DateTime)workerConfiguration.StartDate))
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(timeValue)
                        .RepeatForever())
                    .Build();
        }

        return null;
    }
}