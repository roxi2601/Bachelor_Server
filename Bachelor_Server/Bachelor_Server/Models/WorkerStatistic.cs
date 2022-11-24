using System;
using System.Collections.Generic;

namespace Bachelor_Server.Models
{
    public partial class WorkerStatistic
    {
        public int PkWorkerStatisticsId { get; set; }
        public int FkWorkerId { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? Status { get; set; }

        public virtual Worker FkWorker { get; set; } = null!;
    }
}
