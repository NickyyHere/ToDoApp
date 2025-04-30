using System.ComponentModel.DataAnnotations;

namespace ToDoApp.dto.create 
{
    public class CreateProjectDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public CreateProjectDTO(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}