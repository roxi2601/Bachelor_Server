using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Bachelor_Server.Models;

namespace Bachelor_Server.BusinessLayer.Services.WorkerConfig
{
    public class WorkerConfigService : IWorkerConfigService
    {
        private readonly IWorkerConfigurationRepo _workerRepo;
        private readonly ILogService _log;
        private List<WorkerConfiguration> _workerConfigurations = new();
        public WorkerConfigService(IWorkerConfigurationRepo repo, ILogService log)
        {
            _workerRepo = repo;
            _log = log;
        }

        public async Task CreateWorkerConfiguration(WorkerConfiguration workerConfigurationModel)
        {
            try
            {
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

        public async Task<List<WorkerConfiguration>> GetWorkerConfigurations()
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
    }
    
}