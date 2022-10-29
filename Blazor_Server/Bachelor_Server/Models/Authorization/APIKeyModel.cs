using System.ComponentModel.DataAnnotations;

namespace Bachelor_Server.Models.Authorization
{
    public class APIKeyModel
    {
        [Key] public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string AddTo { get; set; }
    }
}