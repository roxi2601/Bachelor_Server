using System.ComponentModel.DataAnnotations;


namespace Bachelor_Server.Models.Body
{
    public class FormDataModel
    {
        [Key] public int Id { get; set; }
        public int WorkerConfiguationID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}