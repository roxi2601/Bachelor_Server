using System;
using System.Collections.Generic;

namespace Bachelor_Server.Models
{
    public partial class WorkerStatistic
    {
        public WorkerStatistic()
        {
            Workers = new HashSet<Worker>();
        }

        public int PkWorkerStatisticsId { get; set; }
        public int? NumberOfFailedRuns { get; set; }
        public string? LastTimeRun { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }
    }
}
