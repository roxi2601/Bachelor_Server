using System;
using System.Collections.Generic;

namespace Bachelor_Server.Models
{
    public partial class BasicAuth
    {
        public BasicAuth()
        {
            WorkerConfigurations = new HashSet<WorkerConfiguration>();
        }

        public int PkBasicAuthId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<WorkerConfiguration> WorkerConfigurations { get; set; }
    }
}
