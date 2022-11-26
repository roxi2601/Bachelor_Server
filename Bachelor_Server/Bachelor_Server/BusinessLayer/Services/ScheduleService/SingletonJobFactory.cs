using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Quartz;
using Quartz.Spi;


namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public class SingletonJobFactory : IJobFactory
{
    private IServiceProvider _serviceProvider;
    public SingletonJobFactory(IServiceProvider serviceProvider)
    {
        var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection(); 
         serviceCollection.AddSingleton<Job>(); 
         serviceProvider= serviceCollection.BuildServiceProvider(); 
        _serviceProvider = serviceProvider;
    }
    
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
    }

    public void ReturnJob(IJob job)
    {
        
    }
}