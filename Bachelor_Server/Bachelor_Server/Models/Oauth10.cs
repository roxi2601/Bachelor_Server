using System;
using System.Collections.Generic;

namespace Bachelor_Server.Models
{
    public partial class Oauth10
    {
        public Oauth10()
        {
            WorkerConfigurations = new HashSet<WorkerConfiguration>();
        }

        public int PkOauth10id { get; set; }
        public string? SignatureMethod { get; set; }
        public string? ConsumerKey { get; set; }
        public string? ConsumerSecret { get; set; }
        public string? AccessToken { get; set; }
        public string? CallbackUrl { get; set; }
        public string? Timestamp { get; set; }
        public string? Nonce { get; set; }
        public string? Version { get; set; }
        public string? Realm { get; set; }
        public bool? IncludeBodyHash { get; set; }
        public bool? EmptyParamToSig { get; set; }

        public virtual ICollection<WorkerConfiguration> WorkerConfigurations { get; set; }
    }
}
