using System.ComponentModel.DataAnnotations;

namespace Bachelor_Server.Models.WorkerConfiguration
{
    public class ParametersHeaderModel
    {
        [Key] public int ID { get; set; }
        public int WorkerConfig_FK { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}