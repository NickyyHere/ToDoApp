namespace ToDoApp.dto.create 
{
    public class CreateTodoItemDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> TechnologyNames { get; set; }
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