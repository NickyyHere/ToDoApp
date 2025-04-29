namespace ToDoApp.dto.create 
{
    public class CreateTechnologyDTO
    {
        public string Name { get; set; }
        public CreateTechnologyDTO(string name)
        {
            Name = name;
        }
    }
}