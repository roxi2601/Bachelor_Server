

namespace Bachelor_Server.Models
{
    public partial class Oauth20
    {
        public Oauth20()
        {
            WorkerConfigurations = new HashSet<WorkerConfiguration>();
        }

        public int PkOauth20id { get; set; }
        public string? AccessToken { get; set; }
        public string? HeaderPrefix { get; set; }

        public virtual ICollection<WorkerConfiguration> WorkerConfigurations { get; set; }
    }
}
