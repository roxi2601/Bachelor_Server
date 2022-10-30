using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Bachelor_Server.Models.Body;
using Bachelor_Server.Models.General;
using Bachelor_Server.Models.WorkerConfiguration;

namespace Bachelor_Server.BusinessLayer.Services.WorkerConfig
{
    public class WorkerConfigService : IWorkerConfigService
    {
        private readonly IWorkerConfigurationRepo _workerRepo;
        private readonly ILogHandling _log;
        private List<WorkerConfigurationModel> _workerConfigurations = new();

        public WorkerConfigService(IWorkerConfigurationRepo repo, ILogHandling log)
        {
            _workerRepo = repo;
            _log = log;
        }

        public async Task CreateWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel)
        {
            try
            {
                Body(workerConfigurationModel, workerConfigurationModel.Data);
                Auth(workerConfigurationModel, workerConfigurationModel.Data);
                await _workerRepo.CreateWorkerConfiguration(workerConfigurationModel);
                await _log.Log("Object created with url: " + workerConfigurationModel.url);
            }
            catch (Exception e)
            {
                await _log.LogError(e);
            }
        }

        public async Task EditWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel)
        {
            try
            {
                await _workerRepo.EditWorkerConfiguration(workerConfigurationModel);
                await _log.Log("Object edited with id: " + workerConfigurationModel.ID);
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

        public async Task<List<WorkerConfigurationModel>> ReadAllWorkerConfigurations()
        {
            try
            {
                _workerConfigurations = await _workerRepo.GetWorkerConfigurations();
                return _workerConfigurations;
            }
            catch (Exception e)
            {
                await _log.LogError(e);
            }

            return new List<WorkerConfigurationModel>();
        }

        public WorkerConfigurationModel GetWorkerConfigurationById(int id)
        {
            return _workerConfigurations.First(wc => wc.ID == id);
        }

        private void Body(WorkerConfigurationModel workerConfig, WorkerConfigData data)
        {
            switch (data.BodyType)
            {
                case "raw":
                    workerConfig.RawModel.Text = data.Raw;
                    break;
                case "form-data":
                    List<FormDataModel> formData = new List<FormDataModel>();
                    foreach (var item in data.Formdata)
                    {
                        formData.Add(item);
                    }

                    workerConfig.FormDataModel = formData;
                    break;
                default: break;
            }
        }


        private void Auth(WorkerConfigurationModel workerConfig, WorkerConfigData data)
        {
            switch (data.AuthType)
            {
                case "APIKey":
                    workerConfig.ApiKeyModel.AddTo = data.APIKEY_AddTo;
                    workerConfig.ApiKeyModel.Key = data.APIKey_KEY;
                    workerConfig.ApiKeyModel.Value = data.APIKey_Value;
                    break;
                case "BearerToken":

                    workerConfig.BearerTokenModel.Token = data.BearerToken_Token;
                    break;
                case "BasicAuth":
                    workerConfig.BasicAuthModel.Username = data.BasicAuth_Username;
                    workerConfig.BasicAuthModel.Password = data.BasicAuth_Password;
                    break;
                // case "OAuth1": 
                case "OAuth2":
                    workerConfig.OAuth2Model.AccessToken = data.OAuth2_AccessToken;
                    workerConfig.OAuth2Model.HeaderPrefix = data.OAuth2_HeaderPrefix;
                    break;
                default: break;
            }
        }
    }
}