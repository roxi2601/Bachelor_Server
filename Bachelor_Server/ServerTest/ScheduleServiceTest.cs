using Bachelor_Server.BusinessLayer.Services.ScheduleService;
using Bachelor_Server.Models;
using Moq;
using System.Security.Policy;
using Bachelor_Server.BusinessLayer.Services.Email;
using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace ServerTest;

public class ScheduleServiceTest
{
    private Mock<IWorkerConfigurationRepo> _workerConfigurationRepo = new();
    private Mock<ILogService> _logService = new();
    private ISchedulerFactory _schedulerFactory;
    private IScheduler Scheduler;
    private Mock<IServiceScopeFactory> _serviceProvider = new();
    private Mock<IEmailSerivce> _emailSerivce = new();

    [Test]
    public async Task BuildTriggerTest()
    {
        WorkerConfiguration modelMin = new WorkerConfiguration
        {
            Url = "https://catfact.ninja/fact",
            Headers = new List<Header>(),
            Parameters = new List<Parameter>(),
            RequestType = "get",
            LastSavedAuth = "noAuth",
            LastSavedBody = "none",
            ScheduleRate = "1",
            StartDate = DateTime.Now
        };
        
        _schedulerFactory = new StdSchedulerFactory();
        Scheduler = await _schedulerFactory.GetScheduler();

        var job = JobBuilder.Create<Job>()
            .WithIdentity(modelMin.Url)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity(modelMin.Url + " trigger")
            .StartAt(new DateTimeOffset((DateTime)modelMin.StartDate))
            .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(Int32.Parse(modelMin.ScheduleRate))
                .RepeatForever())
            .Build();
        
        await Scheduler.Start();
        await Scheduler.ScheduleJob(job, trigger);
        var exists = await Scheduler.CheckExists(job.Key);
        Assert.True(exists);
        await Scheduler.UnscheduleJob(new TriggerKey("https://catfact.ninja/fact trigger"));
        exists = await Scheduler.CheckExists(job.Key);
        Assert.False(exists);
    }
}