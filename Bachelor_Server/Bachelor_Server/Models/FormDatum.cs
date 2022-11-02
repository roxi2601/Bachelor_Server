using System;
using System.Collections.Generic;

namespace Bachelor_Server.Models
{
    public partial class FormDatum
    {
        public int PkFormDataId { get; set; }
        public int FkWorkerConfigurationId { get; set; }
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string? Description { get; set; }

        public virtual WorkerConfiguration FkWorkerConfiguration { get; set; } = null!;
    }
}
