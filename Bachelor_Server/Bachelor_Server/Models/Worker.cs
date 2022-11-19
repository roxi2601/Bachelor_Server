using System;
using System.Collections.Generic;

namespace Bachelor_Server.Models
{
    public partial class Worker
    {

        public Worker(Type type, string scheduleRate)
        {
            Type = type;
            ScheduleRate = scheduleRate;
        }

        public Type Type;
        public int PkWorkerId { get; set; }
        public int FkWorkerConfigurationId { get; set; }
        public int FkWorkerStatisticsId { get; set; }
        public string? Name { get; set; }
        public string? ScheduleRate { get; set; }
        public bool? IsActive { get; set; }

        public virtual WorkerConfiguration FkWorkerConfiguration { get; set; } = null!;
        public virtual WorkerStatistic FkWorkerStatistics { get; set; } = null!;

    }
}
