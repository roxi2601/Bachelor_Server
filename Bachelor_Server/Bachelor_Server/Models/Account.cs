
namespace Bachelor_Server.Models
{
    public partial class Account
    {
        public int PkAccountId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string Type { get; set; } = null!;
    }
}
