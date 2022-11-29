using Bachelor_Server.Models;

namespace Bachelor_Server.DataAccessLayer.Repositories.Schedule
{
    public interface IStatisticsRepo
    {
        Task ManageStatistic(WorkerStatistic workerStatistic);
        Task<List<WorkerStatistic>> GetStatisticsForWorkerConfiguration(int id);
        Task<List<WorkerStatistic>> GetAllStatistics();
    }
}
