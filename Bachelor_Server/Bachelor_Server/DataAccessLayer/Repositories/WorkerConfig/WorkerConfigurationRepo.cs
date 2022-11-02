using Bachelor_Server.Models;

namespace Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig
{
    public class WorkerConfigurationRepo : IWorkerConfigurationRepo
    {
        private int BodyID;
        private int AuthID;
        private BachelorDBContext dbContext;
        public WorkerConfigurationRepo(BachelorDBContext bachelorDBContext)
        {
            dbContext = bachelorDBContext;
        }

        public async Task CreateWorkerConfiguration(WorkerConfiguration workerConfigurationModel)
        {
            await using (dbContext)
            {
                dbContext.WorkerConfigurations.Add(workerConfigurationModel);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteWorkerConfiguration(int id)
        {
            await using (dbContext)
            {
                var delete = dbContext.WorkerConfigurations.First(x => x.PkWorkerConfigurationId == id);
                if (delete == null)
                {
                    //return Task.CompletedTask;
                }
                dbContext.WorkerConfigurations.Remove(delete);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task EditWorkerConfiguration(WorkerConfiguration workerConfiguration)
        {
            await using (dbContext)
            {
                var dbWorkerConfig = dbContext.WorkerConfigurations
                    .First(x => x.PkWorkerConfigurationId == workerConfiguration.PkWorkerConfigurationId);
                if (dbWorkerConfig == null)
                {
                    //
                }
                dbWorkerConfig.Url = workerConfiguration.Url;
                dbWorkerConfig.RequestType = workerConfiguration.RequestType;
                dbWorkerConfig.LastSavedBody = workerConfiguration.LastSavedBody;
                dbWorkerConfig.LastSavedAuth = workerConfiguration.LastSavedAuth;
                dbWorkerConfig.FkApikey = workerConfiguration.FkApikey;
                dbWorkerConfig.FkBasicAuth = workerConfiguration.FkBasicAuth;
                dbWorkerConfig.FkBearerToken = workerConfiguration.FkBearerToken;
                dbWorkerConfig.FkOauth10 = workerConfiguration.FkOauth10;
                dbWorkerConfig.FkOauth20 = workerConfiguration.FkOauth20;
                dbWorkerConfig.FkRaw = workerConfiguration.FkRaw;
                dbWorkerConfig.FormData = workerConfiguration.FormData;
                dbWorkerConfig.Headers = workerConfiguration.Headers;
                dbWorkerConfig.Parameters = workerConfiguration.Parameters;
                dbContext.Update(dbWorkerConfig);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<WorkerConfiguration>> GetWorkerConfigurations()
        {
            await using (dbContext)
            {
                var workerConfigurations = dbContext.WorkerConfigurations
                    /*.Include(x => x.FormData)
                    .Include(x => x.Headers)
                    .Include(x => x.Parameters)
                    .Include(x => x.FkApikey)
                    .Include(x => x.FkBearerToken)
                    .Include(x => x.FkBasicAuth)
                    .Include(x => x.FkOauth10)
                    .Include(x => x.FkOauth20)
                    .Include(x => x.FkRaw)*/
                    .ToList();
                /*                foreach (WorkerConfiguration wc in workerConfigurations)
                                {
                                    Console.WriteLine(wc.Url);
                                }*/
                return workerConfigurations;
            }
        }
    }
}

