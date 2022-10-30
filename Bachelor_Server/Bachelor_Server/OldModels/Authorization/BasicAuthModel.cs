using System.ComponentModel.DataAnnotations;


namespace Bachelor_Server.OldModels.Authorization
{
    public class BasicAuthModel
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
