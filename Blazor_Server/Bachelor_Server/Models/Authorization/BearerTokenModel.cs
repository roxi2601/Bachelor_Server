using System.ComponentModel.DataAnnotations;


namespace Bachelor_Server.Models.Authorization
{
    public class BearerTokenModel
    {
        [Key] public int Id { get; set; }
        public string Token { get; set; }
    }
}