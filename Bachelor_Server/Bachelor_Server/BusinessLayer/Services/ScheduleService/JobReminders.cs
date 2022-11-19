using Quartz;

namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public class JobReminders : IJob
{
    public JobReminders()
    {
        
    }
    
    public Task Execute(IJobExecutionContext context)
    {
       Console.WriteLine("IN EXECUTE");
       return Task.CompletedTask;
    }
}