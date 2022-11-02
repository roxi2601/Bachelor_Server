﻿using System;
using System.Collections.Generic;

namespace Bachelor_Server.Models
{
    public partial class Apikey
    {
        public Apikey()
        {
            WorkerConfigurations = new HashSet<WorkerConfiguration>();
        }

        public int PkApikeyId { get; set; }
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string? AddTo { get; set; }

        public virtual ICollection<WorkerConfiguration> WorkerConfigurations { get; set; }
    }
}
