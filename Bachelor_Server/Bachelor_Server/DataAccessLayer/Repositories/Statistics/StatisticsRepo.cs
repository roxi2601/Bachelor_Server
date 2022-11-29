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

        public async Task<List<WorkerStatistic>> GetAllStatistics()
        {
            await using (var context = await _dbContext.CreateDbContextAsync())
            {
                return await context.WorkerStatistics.ToListAsync();
            }
        }

        public async Task<List<WorkerStatistic>> GetStatisticsForWorkerConfiguration(int id)
        {
            await using (var context = await _dbContext.CreateDbContextAsync())
            {
                return await context.WorkerStatistics.Where(x => x.FkWorkerConfigurationId == id).ToListAsync();
            }
        }

    }
}
