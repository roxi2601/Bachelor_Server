﻿using Bachelor_Server.Models;

namespace Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig
{
    public interface IWorkerConfigurationRepo
    {
        Task CreateWorkerConfiguration(WorkerConfiguration workerConfigurationModel);

        Task<List<WorkerConfiguration>> GetWorkerConfigurations();

        Task EditWorkerConfiguration(WorkerConfiguration workerConfigurationModel);

        Task DeleteWorkerConfiguration(int id);

        Task<WorkerConfiguration> GetWorkerConfiguration(int id);

        Task EditSchedule(WorkerConfiguration workerConfiguration);
        Task<List<WorkerConfiguration>> GetActiveWorkerConfigurations();
        Task<List<WorkerConfiguration>> GetWorkerConfigurationsWithStatistics();
    }
}
