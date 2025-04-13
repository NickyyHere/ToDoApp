using ToDoApp.enumerable.Status;
using ToDoApp.models.Technology;

namespace ToDoApp.dto
{
    namespace TodoItemDTO
    {
        public class TodoItemDTO
        {
            public string ProjectName { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public Status Status { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? FinishDate { get; set; }
            public ICollection<Technology> Technologies { get; set; }

            public TodoItemDTO(string projectName, string name, string description, Status status, ICollection<Technology> technologies, DateTime startDate, DateTime? finishDate = null) {
                ProjectName = projectName;
                Name = name;
                Description = description;
                Status = status;
                StartDate = startDate;
                FinishDate = finishDate;
                Technologies = technologies;
            }
        }

        public class TodoItemWithProjectIdDTO
        {
            public TodoItemDTO Item { get; set; }
            public int ProjectId { get; set; }

            public TodoItemWithProjectIdDTO(TodoItemDTO todoItemDTO, int projectId)
            {
                Item = todoItemDTO;
                ProjectId = projectId;
            }
        }
    }
}
