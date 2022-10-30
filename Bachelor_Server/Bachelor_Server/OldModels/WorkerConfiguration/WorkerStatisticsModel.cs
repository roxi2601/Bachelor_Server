using System.ComponentModel.DataAnnotations;

namespace Bachelor_Server.OldModels.WorkerConfiguration
{
    public class WorkerStatisticsModel
    {
        [Key] public int Id { get; set; }
        public string NumberOfFailedRuns { get; set; }
        public string LastTimeRun { get; set; }
        public string WorkerId { get; set; }
    }
}