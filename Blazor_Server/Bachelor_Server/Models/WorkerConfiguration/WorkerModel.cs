using System.ComponentModel.DataAnnotations;

namespace Bachelor_Server.Models.WorkerConfiguration
{
    public class WorkerModel
    {
        [Key] public int Id { get; set; }
        public string WorkerConfigurationId { get; set; }
        public string Name { get; set; }
        public string ScheduleRate { get; set; }
        public string IsActive { get; set; }
    }
}