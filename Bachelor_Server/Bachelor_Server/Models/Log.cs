
namespace Bachelor_Server.Models
{
    public partial class Log
    {
        public int PkLogId { get; set; }
        public string Date { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? StackTrace { get; set; }
    }
}
