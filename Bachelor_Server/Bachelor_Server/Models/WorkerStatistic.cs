

namespace Bachelor_Server.Models
{
    public partial class WorkerStatistic
    {
        public int PkWorkerStatisticsId { get; set; }
        public int FkWorkerConfigurationId { get; set; }
        public int NumberOfSuccesfulRuns { get; set; }
        public int NumberOfFailedRuns { get; set; }
        public DateTime LastRunTime { get; set; }
        public decimal LastRunTimeLengthSec { get; set; }

        public virtual WorkerConfiguration FkWorkerConfiguration { get; set; } = null!;
    }
}
