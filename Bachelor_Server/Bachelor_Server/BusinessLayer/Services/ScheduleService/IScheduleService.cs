using Bachelor_Server.Models;
using Quartz;

namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;

public interface IScheduleService
{
    Task ScheduleWorkerConfiguration(WorkerConfiguration workerConfiguration); //TODO: think of the name create or sth els

    WorkerConfiguration GetWorkerConfig();
}