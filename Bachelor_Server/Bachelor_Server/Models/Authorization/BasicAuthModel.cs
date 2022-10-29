using System.ComponentModel.DataAnnotations;


namespace Bachelor_Server.Models.Authorization
{
    public class BasicAuthModel
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
