using ToDoApp.enumerable.Status;

namespace ToDoApp.dto
{
    namespace ProjectDTO
    {
        public class ProjectDTO
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime StartDate { get; set;}
            public DateTime? FinishDate { get; set;}
            public Status Status { get; set; }

            public ProjectDTO(string name, string description, Status status, DateTime dateTime, DateTime? finishDate = null) {
                Name = name;
                Description = description;
                StartDate = dateTime;
                FinishDate = finishDate;
                Status = status;
            }
        }
    }
}
