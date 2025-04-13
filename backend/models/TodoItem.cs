using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.enumerable.Status;

namespace ToDoApp.models
{
    namespace TodoItem {
        [Table("TodoItems")]
        public class TodoItem {
            [Key]
            public int Id { get; set; }
            [Column("Project_id")]
            public Project.Project Project { get; set; }
            [Column("Name")]
            public string Name { get; set; }
            [Column("Description", TypeName = "text")]
            public string Description { get; set; }
            [Column("Status", TypeName = "varchar(20)")]
            public Status Status { get; set; }
            [Column("StartDate", TypeName = "timestamp")]
            public DateTime StartDate { get; set; }
            [Column("FinishDate", TypeName = "timestamp")]
            public DateTime? FinishDate { get; set; }
            public ICollection<Technology.Technology> Technologies { get; set; }

            public TodoItem(string name, string description, List<Technology.Technology> technologies, Project.Project project) {
                Name = name;
                Description = description;
                Status = Status.NEW;
                StartDate = DateTime.Now;
                Technologies = technologies;
                Project = project;
            }

            public TodoItem() {
                Name = Description = string.Empty;
                Status = Status.NEW;
                StartDate = DateTime.Now;
                Technologies = [];
                Project = new Project.Project();
            }
        }
    }
}

