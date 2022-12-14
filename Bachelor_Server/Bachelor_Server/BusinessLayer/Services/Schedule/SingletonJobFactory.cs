using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Quartz;
using Quartz.Spi;


namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public class SingletonJobFactory : IJobFactory
{
    private IServiceScopeFactory _serviceProvider;
    public SingletonJobFactory(IServiceScopeFactory serviceProvider)
    {
        // var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection(); 
        //  serviceCollection.AddSingleton<Job>(); 
        //  serviceProvider= serviceCollection.BuildServiceProvider(); 
        _serviceProvider = serviceProvider;
    }
    
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            return scope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
        }
    }

    public void ReturnJob(IJob job)
    {
        
    }
}