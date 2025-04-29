namespace ToDoApp.dto.create 
{
    public class CreateProjectDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CreateProjectDTO(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}