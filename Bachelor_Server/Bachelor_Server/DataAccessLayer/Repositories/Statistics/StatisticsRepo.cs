using Bachelor_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Bachelor_Server.DataAccessLayer.Repositories.Schedule
{
    public class StatisticsRepo : IStatisticsRepo
    {
        private IDbContextFactory<BachelorDBContext> _dbContext;
        public StatisticsRepo(IDbContextFactory<BachelorDBContext> bachelorDBContext)
        {
            _dbContext = bachelorDBContext;
        }
        
        public async Task ManageStatistic(WorkerStatistic workerStatistic)
        {
            await using (var context = await _dbContext.CreateDbContextAsync())
            {
                var result = await context.WorkerStatistics.FirstOrDefaultAsync(x => x.FkWorkerConfigurationId == workerStatistic.FkWorkerConfigurationId);
                if (result == null)
                {
                    await context.WorkerStatistics.AddAsync(workerStatistic);
                }
                else
                {
                    result.NumberOfFailedRuns= workerStatistic.NumberOfFailedRuns;
                    result.NumberOfSuccesfulRuns= workerStatistic.NumberOfSuccesfulRuns;
                    result.LastRunTime= workerStatistic.LastRunTime;
                    result.LastRunTimeLengthSec= workerStatistic.LastRunTimeLengthSec;
                    context.WorkerStatistics.Update(result);
                }
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<WorkerStatistic>> GetAllStatistics()
        {
            await using (var context = await _dbContext.CreateDbContextAsync())
            {
                return await context.WorkerStatistics.ToListAsync();
            }
        }

        public async Task<WorkerStatistic> GetStatisticsForWorkerConfiguration(int id)
        {
            await using (var context = await _dbContext.CreateDbContextAsync())
            {
                return context.WorkerStatistics.First(x => x.FkWorkerConfigurationId == id);
            }
        }

    }
}
