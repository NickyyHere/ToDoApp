using System.ComponentModel.DataAnnotations;

namespace ToDoApp.dto.create 
{
    public class CreateTechnologyDTO
    {
        [Required]
        public string Name { get; set; }
        public CreateTechnologyDTO(string name)
        {
            Name = name;
        }
    }
}