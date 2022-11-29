using Bachelor_Server.Models;

namespace Bachelor_Server.BusinessLayer.Services.Statistics
{
    public interface IStatisticsService
    {
        Task<List<WorkerStatistic>> GetStatisticsForWorkerConfiguration(int id);
        Task<List<WorkerStatistic>> GetAllStatistics();
    }
}
