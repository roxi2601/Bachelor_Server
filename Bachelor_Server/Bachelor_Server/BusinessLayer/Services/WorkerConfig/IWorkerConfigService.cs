using Bachelor_Server.Models;


namespace Bachelor_Server.BusinessLayer.Services.WorkerConfig
{
    public interface IWorkerConfigService
    {
        Task CreateWorkerConfiguration(WorkerConfiguration workerConfigurationModel);
        Task EditWorkerConfiguration(WorkerConfiguration workerConfigurationModel);
        Task DeleteWorkerConfiguration(int id);
        Task<List<WorkerConfiguration>> GetWorkerConfigurations();
        WorkerConfiguration GetWorkerConfigurationById(int id);
        
    }
}