using Bachelor_Server.Models;

namespace Bachelor_Server.DataAccessLayer.Repositories.Schedule
{
    public interface IStatisticsRepo
    {
        Task<List<WorkerStatistic>> GetStatisticsForWorkerConfiguration(int id);
        Task<List<WorkerStatistic>> GetAllStatistics();
    }
}
