using System.ComponentModel.DataAnnotations;

namespace ToDoApp.dto.create 
{
    public class CreateTodoItemDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public List<string> TechnologyNames { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public CreateTodoItemDTO(string name, string description, List<string> technologyNames, int projectId)
        {
            Name = name;
            Description = description;
            TechnologyNames = technologyNames;
            ProjectId = projectId;
        }
    }
}