using System;
using System.Collections.Generic;

namespace Bachelor_Server.Models
{
    public partial class Raw
    {
        public Raw()
        {
            WorkerConfigurations = new HashSet<WorkerConfiguration>();
        }

        public int PkRawId { get; set; }
        public string? Text { get; set; }

        public virtual ICollection<WorkerConfiguration> WorkerConfigurations { get; set; }
    }
}
