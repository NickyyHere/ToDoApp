using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.models 
{
    [Table("TodoTechnologies")]
    public class TodoTechnology
    {
        public int ItemId { get; set; }
        public TodoItem Item { get; set; } = new();
        public int TechnologyId { get; set; }
        public Technology Technology { get; set; } = new();
    }
}