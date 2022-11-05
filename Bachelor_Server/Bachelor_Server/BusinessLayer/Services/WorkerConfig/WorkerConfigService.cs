using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Bachelor_Server.Models;

namespace Bachelor_Server.BusinessLayer.Services.WorkerConfig
{
    public class WorkerConfigService : IWorkerConfigService
    {
        private readonly IWorkerConfigurationRepo _workerRepo;
        private readonly ILogHandling _log;
        private List<WorkerConfiguration> _workerConfigurations = new();
        public WorkerConfigService(IWorkerConfigurationRepo repo, ILogHandling log)
        {
            _workerRepo = repo;
            _log = log;
        }

        public async Task CreateWorkerConfiguration(WorkerConfiguration workerConfigurationModel)
        {
            try
            {
                // Body(workerConfigurationModel, workerConfigurationModel.Data);
                // Auth(workerConfigurationModel, workerConfigurationModel.Data);
                await _workerRepo.CreateWorkerConfiguration(workerConfigurationModel);
                await _log.Log("Object created with url: " + workerConfigurationModel.Url);
            }
            catch (Exception e)
            {
                await _log.LogError(e);
            }
        }

        public async Task EditWorkerConfiguration(WorkerConfiguration workerConfigurationModel)
        {
            try
            {
                await _workerRepo.EditWorkerConfiguration(workerConfigurationModel);
                await _log.Log("Object edited with id: " + workerConfigurationModel.PkWorkerConfigurationId);
            }
            catch (Exception e)
            {
                await _log.LogError(e);
            }
        }

        public async Task DeleteWorkerConfiguration(int id)
        {
            try
            {
                await _workerRepo.DeleteWorkerConfiguration(id);
                await _log.Log("Object deleted with id: " + id);
            }
            catch (Exception e)
            {
                await _log.LogError(e);
            }
           
        }

        public async Task<List<WorkerConfiguration>> ReadAllWorkerConfigurations()
        {
            try
            {
                return _workerConfigurations = await _workerRepo.GetWorkerConfigurations();
            }
            catch (Exception e)
            {
                await _log.LogError(e);
            }

            return new List<WorkerConfiguration>();
        }

        public WorkerConfiguration GetWorkerConfigurationById(int id)
        {
            return _workerConfigurations.First(wc => wc.PkWorkerConfigurationId == id);
        }

        // private void Body(WorkerConfiguration workerConfig, WorkerConfigData data)
        // {
        //     switch (data.BodyType)
        //     {
        //         case "raw":
        //             workerConfig.FkRaw.Text = data.Raw;
        //             break;
        //         case "form-data":
        //             List<FormDatum> formData = new List<FormDatum>();
        //             foreach (var item in data.Formdata)
        //             {
        //                 formData.Add(item);
        //             }
        //
        //             workerConfig.FormData = formData;
        //             break;
        //         default: break;
        //     }
        // }
        //
        //
        // private void Auth(WorkerConfiguration workerConfig)
        // {
        //     switch (workerConfig.LastSavedAuth)
        //     {
        //         case "APIKey":
        //             workerConfig.FkApikey.AddTo = workerConfig.FkApikey.AddTo;
        //             workerConfig.FkApikey.Key = workerConfig.FkApikey.Key;
        //             workerConfig.FkApikey.Value = workerConfig.FkApikey.Value;
        //             break;
        //         case "BearerToken":
        //
        //             workerConfig.FkBearerToken.Token = workerConfig.FkBearerToken.BearerToken_Token;
        //             break;
        //         case "BasicAuth":
        //             workerConfig.FkBasicAuth.Username = data.BasicAuth_Username;
        //             workerConfig.FkBasicAuth.Password = data.BasicAuth_Password;
        //             break;
        //         // case "OAuth1": 
        //         case "OAuth2":
        //             workerConfig.FkOauth20.AccessToken = data.OAuth2_AccessToken;
        //             workerConfig.FkOauth20.HeaderPrefix = data.OAuth2_HeaderPrefix;
        //             break;
        //         default: break;
        //     }
        }
    
}