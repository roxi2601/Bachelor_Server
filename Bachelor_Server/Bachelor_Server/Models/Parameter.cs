

namespace Bachelor_Server.Models
{
    public partial class Parameter
    {
        public int PkParameterId { get; set; }
        public int FkWorkerConfigurationId { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? Description { get; set; }

        public virtual WorkerConfiguration FkWorkerConfiguration { get; set; } = null!;
    }
}
