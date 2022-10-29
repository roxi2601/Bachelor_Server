
using Bachelor_Server.Models.WorkerConfiguration;

namespace Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig
{
    public interface IWorkerConfigurationRepo
    {
        Task CreateWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel);

        Task<List<WorkerConfigurationModel>> GetWorkerConfigurations();

        Task EditWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel);

        Task DeleteWorkerConfiguration(int id);
    }
}
