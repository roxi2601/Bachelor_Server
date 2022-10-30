using System.ComponentModel.DataAnnotations;


namespace Bachelor_Server.OldModels.Body
{
    public class RawModel
    {
        [Key] public int Id { get; set; }
        public string Text { get; set; }
    }
}