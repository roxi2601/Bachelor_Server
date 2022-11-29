using Bachelor_Server.Models;

namespace Bachelor_Server.DataAccessLayer.Repositories.Schedule
{
    public interface IStatisticsRepo
    {
        Task ManageStatistic(WorkerStatistic workerStatistic);
        Task<WorkerStatistic> GetStatisticsForWorkerConfiguration(int id);
        Task<List<WorkerStatistic>> GetAllStatistics();
    }
}
