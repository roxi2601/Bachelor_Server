using Quartz;

namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public class RESTCall : IJob
{
    public RESTCall()
    {
        
    }
    
    public Task Execute(IJobExecutionContext context)
    {
       Console.WriteLine("IN EXECUTE");
       return Task.CompletedTask;
    }
}