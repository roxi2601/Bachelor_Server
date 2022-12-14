using Bachelor_Server.Models;

namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public interface IScheduleService
{
    Task ScheduleWorkerConfiguration(WorkerConfiguration workerConfiguration);

    Task RunAllSchedules();
}