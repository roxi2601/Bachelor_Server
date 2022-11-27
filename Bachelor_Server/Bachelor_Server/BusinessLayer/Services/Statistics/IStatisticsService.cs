﻿using Bachelor_Server.Models;

namespace Bachelor_Server.BusinessLayer.Services.Statistics
{
    public interface IStatisticsService
    {
        Task CreateStatistics(WorkerStatistic workerStatistic);
        Task<List<WorkerStatistic>> GetStatisticsForWorkerConfiguration(int id);
        Task<List<WorkerStatistic>> GetAllStatistics();
    }
}