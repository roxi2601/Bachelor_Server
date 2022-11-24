using Bachelor_Server.Models;
using Quartz;

namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public interface IScheduleService
{
    Task CreateWorker(Worker worker); //TODO: think of the name create or sth else
    
}