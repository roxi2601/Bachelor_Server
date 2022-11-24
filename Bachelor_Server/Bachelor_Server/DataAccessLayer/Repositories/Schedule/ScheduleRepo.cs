using Bachelor_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Bachelor_Server.DataAccessLayer.Repositories.Schedule
{
    public class ScheduleRepo : IScheduleRepo
    {
        private IDbContextFactory<BachelorDBContext> _dbContext;
        public ScheduleRepo(IDbContextFactory<BachelorDBContext> bachelorDBContext)
        {
            _dbContext = bachelorDBContext;
        }

        public async Task CreateStatistics(WorkerStatistic workerStatistic)
        {
            await using (var context = await _dbContext.CreateDbContextAsync())
            {
                await context.WorkerStatistics.AddAsync(workerStatistic);
                await context.SaveChangesAsync();
            }
        }

        public async Task CreateWorker(Worker worker)
        {
            await using (var context = await _dbContext.CreateDbContextAsync())
            {
                await context.Workers.AddAsync(worker);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteWorker(int id)
        {
            await using (var context = await _dbContext.CreateDbContextAsync())
            {
                var delete = await context.Workers.FirstAsync(x => x.PkWorkerId == id);
                context.Workers.Remove(delete);
                await context.SaveChangesAsync();
            }
        }

        public async Task EditWorker(Worker worker)
        {
            await using (var context = await _dbContext.CreateDbContextAsync())
            {
                context.Workers.Update(worker);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<WorkerStatistic>> GetStatisticsForWorker(int id)
        {
            await using (var context = await _dbContext.CreateDbContextAsync())
            {
                return await context.WorkerStatistics.Where(x => x.FkWorkerId == id).ToListAsync();
            }
        }

        public async Task<Worker> GetWorkerById(int id)
        {
            await using (var context = await _dbContext.CreateDbContextAsync())
            {
                return await context.Workers.FirstAsync(x => x.PkWorkerId == id);
            }
        }

        public async Task<List<Worker>> GetWorkers()
        {
            await using (var context = await _dbContext.CreateDbContextAsync())
            {
                return await context.Workers.ToListAsync();
            }
        }
    }
}
