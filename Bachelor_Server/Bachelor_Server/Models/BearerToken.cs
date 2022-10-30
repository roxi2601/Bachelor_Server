using System;
using System.Collections.Generic;

namespace Bachelor_Server.Models
{
    public partial class BearerToken
    {
        public BearerToken()
        {
            WorkerConfigurations = new HashSet<WorkerConfiguration>();
        }

        public int PkBearerTokenId { get; set; }
        public string? Token { get; set; }

        public virtual ICollection<WorkerConfiguration> WorkerConfigurations { get; set; }
    }
}
