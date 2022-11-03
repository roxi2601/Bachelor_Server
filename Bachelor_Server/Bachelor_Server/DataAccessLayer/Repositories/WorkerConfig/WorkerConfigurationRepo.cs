using Bachelor_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig
{
    public class WorkerConfigurationRepo : IWorkerConfigurationRepo
    {
        private int BodyID;
        private int AuthID;
        private IDbContextFactory<BachelorDBContext> dbContext;
        public WorkerConfigurationRepo(IDbContextFactory<BachelorDBContext> bachelorDBContext)
        {
            dbContext = bachelorDBContext;
        }

        public async Task CreateWorkerConfiguration(WorkerConfiguration workerConfigurationModel)
        {
            await using (var context = await dbContext.CreateDbContextAsync())
            {
                await context.Apikeys.AddAsync(workerConfigurationModel.FkApikey);
                /*await context.BearerTokens.AddAsync(workerConfigurationModel.FkBearerToken);
                await context.BasicAuths.AddAsync(workerConfigurationModel.FkBasicAuth);
                await context.Oauth10s.AddAsync(workerConfigurationModel.FkOauth10);
                await context.Oauth20s.AddAsync(workerConfigurationModel.FkOauth20);
                await context.Raws.AddAsync(workerConfigurationModel.FkRaw);*/
                await context.SaveChangesAsync();

                /*var apiKeyID = workerConfigurationModel.FkApikey.PkApikeyId;
                var bearerTokenID = workerConfigurationModel.FkBearerToken.PkBearerTokenId;
                var basicAuthID = workerConfigurationModel.FkBasicAuth.PkBasicAuthId;
                var oAuth1ID = workerConfigurationModel.FkOauth10.PkOauth10id;
                var oAuth2ID = workerConfigurationModel.FkOauth20.PkOauth20id;
                var rawID = workerConfigurationModel.FkRaw.PkRawId;*/

                await context.WorkerConfigurations.AddAsync(workerConfigurationModel);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteWorkerConfiguration(int id)
        {
            await using (var context = await dbContext.CreateDbContextAsync())
            {
                var delete = context.WorkerConfigurations.First(x => x.PkWorkerConfigurationId == id);
                if (delete == null)
                {
                    //return Task.CompletedTask;
                }
                context.WorkerConfigurations.Remove(delete);
                await context.SaveChangesAsync();
            }
        }

        public async Task EditWorkerConfiguration(WorkerConfiguration workerConfiguration)
        {
            await using (var context = await dbContext.CreateDbContextAsync())
            {
                var dbWorkerConfig = context.WorkerConfigurations
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
                context.Update(dbWorkerConfig);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<WorkerConfiguration>> GetWorkerConfigurations()
        {
            await using (var context = await dbContext.CreateDbContextAsync())
            {
                var workerConfigurations = await context.WorkerConfigurations
                    /*.Include(x => x.FormData)
                    .Include(x => x.Headers)
                    .Include(x => x.Parameters)
                    .Include(x => x.FkApikey)
                    .Include(x => x.FkBearerToken)
                    .Include(x => x.FkBasicAuth)
                    .Include(x => x.FkOauth10)
                    .Include(x => x.FkOauth20)
                    .Include(x => x.FkRaw)*/
                    .ToListAsync();
                /*                foreach (WorkerConfiguration wc in workerConfigurations)
                                {
                                    Console.WriteLine(wc.Url);
                                }*/
                return workerConfigurations;
            }
        }
    }
}

