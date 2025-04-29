namespace ToDoApp.dto
{
    public class TechnologyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TechnologyDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}