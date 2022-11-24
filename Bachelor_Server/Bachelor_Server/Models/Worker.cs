﻿using System;
using System.Collections.Generic;
using Quartz;

namespace Bachelor_Server.Models
{
    public partial class Worker
        : IJob
    {
        public int PkWorkerId { get; set; }
        public int FkWorkerConfigurationId { get; set; }
        public int FkWorkerStatisticsId { get; set; }
        public string? Name { get; set; }
        public string? ScheduleRate { get; set; }
        public bool? IsActive { get; set; }

        public virtual WorkerConfiguration FkWorkerConfiguration { get; set; } = null!;
        public virtual WorkerStatistic FkWorkerStatistics { get; set; } = null!;
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("PLEASE WORK ZZZZZZZZZ");
            return Task.CompletedTask;
        }
    }
}