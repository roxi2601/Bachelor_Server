
using Bachelor_Server.Models;
using Bachelor_Server.OldModels.WorkerConfiguration;

namespace Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig
{
    public interface IWorkerConfigurationRepo
    {
        Task CreateWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel);

        Task<List<WorkerConfigurationModel>> GetWorkerConfigurations();
        Task<List<WorkerConfiguration>> NewGetWorkerConfigurations();

        Task EditWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel);

        Task DeleteWorkerConfiguration(int id);
    }
}
