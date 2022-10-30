using System;
using System.Collections.Generic;

namespace Bachelor_Server.Models
{
    public partial class Worker
    {
        public Worker()
        {
            Logs = new HashSet<Log>();
        }

        public int PkWorkerId { get; set; }
        public int FkWorkerConfigurationId { get; set; }
        public int FkWorkerStatisticsId { get; set; }
        public int FkAccountId { get; set; }
        public string? Name { get; set; }
        public string? ScheduleRate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Account FkAccount { get; set; } = null!;
        public virtual WorkerConfiguration FkWorkerConfiguration { get; set; } = null!;
        public virtual WorkerStatistic FkWorkerStatistics { get; set; } = null!;
        public virtual ICollection<Log> Logs { get; set; }
    }
}
