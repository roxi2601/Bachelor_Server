
using Bachelor_Server.Models;


namespace Bachelor_Server.BusinessLayer.Services.WorkerConfig
{
    public interface IWorkerConfigService
    {
        
        Task CreateWorkerConfiguration(WorkerConfiguration workerConfigurationModel);

        Task EditWorkerConfiguration(WorkerConfiguration workerConfigurationModel);

        Task DeleteWorkerConfiguration(int id);

        Task<List<WorkerConfiguration>> ReadAllWorkerConfigurations();

        WorkerConfiguration GetWorkerConfigurationById(int id);
        
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