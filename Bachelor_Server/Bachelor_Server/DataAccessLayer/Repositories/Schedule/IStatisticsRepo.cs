using Bachelor_Server.Models;

namespace Bachelor_Server.DataAccessLayer.Repositories.Schedule
{
    public interface IStatisticsRepo
    {
        Task CreateStatistics(WorkerStatistic workerStatistic);
        Task<List<WorkerStatistic>> GetStatisticsForWorkerConfiguration(int id);
    }
}
