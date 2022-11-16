using System;
using System.Collections.Generic;

namespace Bachelor_Server.Models
{
    public partial class WorkerConfiguration
    {
        public WorkerConfiguration()
        {
            FormData = new HashSet<FormDatum>();
            Headers = new HashSet<Header>();
            Parameters = new HashSet<Parameter>();
            Workers = new HashSet<Worker>();
        }

        public int PkWorkerConfigurationId { get; set; }
        public string Url { get; set; } = null!;
        public string RequestType { get; set; } = null!;
        public string? LastSavedBody { get; set; }
        public int? FkRawId { get; set; }
        public string? LastSavedAuth { get; set; }
        public int? FkBasicAuthId { get; set; }
        public int? FkBearerTokenId { get; set; }
        public int? FkApikeyId { get; set; }
        public int? FkOauth20id { get; set; }

        public virtual Apikey? FkApikey { get; set; }
        public virtual BasicAuth? FkBasicAuth { get; set; }
        public virtual BearerToken? FkBearerToken { get; set; }
        public virtual Oauth20? FkOauth20 { get; set; }
        public virtual Raw? FkRaw { get; set; }
        public virtual ICollection<FormDatum> FormData { get; set; }
        public virtual ICollection<Header> Headers { get; set; }
        public virtual ICollection<Parameter> Parameters { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
