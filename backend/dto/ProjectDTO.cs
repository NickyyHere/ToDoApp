using ToDoApp.enumerable;

namespace ToDoApp.dto
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set;}
        public DateTime? FinishDate { get; set;}
        public Status Status { get; set; }

        public ProjectDTO(int id, string name, string description, Status status, DateTime startDate, DateTime? finishDate = null) {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            FinishDate = finishDate;
            Status = status;
        }
    }
}
