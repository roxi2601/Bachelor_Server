using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.DataAccessLayer.Repositories.Schedule;
using Bachelor_Server.Models;

namespace Bachelor_Server.BusinessLayer.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepo _statRepo;
        private readonly ILogService _log;
        public StatisticsService(IStatisticsRepo stat, ILogService log)
        {
            _statRepo = stat;
            _log = log;
        }

        public async Task ManageStatistic(WorkerStatistic workerStatistic)
        {
            try
            {
                await _statRepo.ManageStatistic(workerStatistic);
                await _log.Log("Statistic updated");
            }
            catch (Exception e)
            {
                await _log.LogError(e);
            }
        }
        
        public async Task<List<WorkerStatistic>> GetAllStatistics()
        {
            try
            {
                return await _statRepo.GetAllStatistics();
            }
            catch (Exception e)
            {
                await _log.LogError(e);
            }
            return new List<WorkerStatistic>();
        }

        public async Task<WorkerStatistic> GetStatisticsForWorkerConfiguration(int id)
        {
            try
            {
                return await _statRepo.GetStatisticsForWorkerConfiguration(id);
            }
            catch (Exception e)
            {
                await _log.LogError(e);
            }

            return new WorkerStatistic();
        }
    }
}
