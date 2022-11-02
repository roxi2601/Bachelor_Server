
using Bachelor_Server.Models;
using Bachelor_Server.OldModels.WorkerConfiguration;

namespace Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig
{
    public interface IWorkerConfigurationRepo
    {
        Task CreateWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel);

        Task<List<WorkerConfiguration>> GetWorkerConfigurations();

        Task EditWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel);

        Task DeleteWorkerConfiguration(int id);
    }
}
