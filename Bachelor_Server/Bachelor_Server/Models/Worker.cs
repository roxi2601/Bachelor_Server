using System;
using System.Collections.Generic;

namespace Bachelor_Server.Models
{
    public partial class Worker
    {
        public Worker()
        {
            WorkerStatistics = new HashSet<WorkerStatistic>();
        }

        public int PkWorkerId { get; set; }
        public int FkWorkerConfigurationId { get; set; }
        public string? Name { get; set; }
        public string? ScheduleRate { get; set; }
        public bool? IsActive { get; set; }

        public virtual WorkerConfiguration FkWorkerConfiguration { get; set; } = null!;
        public virtual ICollection<WorkerStatistic> WorkerStatistics { get; set; }
    }
}
