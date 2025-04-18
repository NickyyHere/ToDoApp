using ToDoApp.enumerable;
using ToDoApp.models;

namespace ToDoApp.dto
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public List<string> Technologies { get; set; }

        public TodoItemDTO(int id, string projectName, string name, string description, Status status, List<string> technologies, DateTime startDate, DateTime? finishDate = null) {
            Id = id;
            ProjectName = projectName;
            Name = name;
            Description = description;
            Status = status;
            StartDate = startDate;
            FinishDate = finishDate;
            Technologies = technologies;
        }

        public override string ToString()
        {
            return 
            $"Id: {Id}\nProject: {ProjectName}\nName: {Name}\nDescription: {Description}\nStatus: {Status}\nTechnologies: {Technologies}\nStartDate: {StartDate}\nFinishDate: {FinishDate}";
        }
    }

}
