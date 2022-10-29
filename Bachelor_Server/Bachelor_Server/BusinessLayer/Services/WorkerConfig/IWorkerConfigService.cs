
using Bachelor_Server.Models.WorkerConfiguration;

namespace Bachelor_Server.BusinessLayer.Services.WorkerConfig
{
    public interface IWorkerConfigService
    {
        
        Task CreateWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel);

        Task EditWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel);

        Task DeleteWorkerConfiguration(int id);

        Task<List<WorkerConfigurationModel>> ReadAllWorkerConfigurations();

        WorkerConfigurationModel GetWorkerConfigurationById(int id);
        
        // Task CreateWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel);
        //
        // Task EditWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel);
        //
        // Task DeleteWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel);
        //
        // Task<List<WorkerConfigurationModel>> ReadAllWorkerConfigurations();
        //
        // WorkerConfigurationModel GetWorkerConfigurationById(int id);
    }
}