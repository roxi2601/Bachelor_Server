﻿

namespace Bachelor_Server.OldModels.LogHandling
{
    public class JsonMessage
    {
        public int StatusCode { get; set; }
        public string Description { get; set; }
        public string Exception { get; set; }
        public DateTime Date { get; set; }
    }
}