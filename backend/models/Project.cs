using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.enumerable;

namespace ToDoApp.models 
{
    [Table("Projects")]
    public class Project {
        [Key]
        public int Id { get; set; }
        
        [Column("Name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Column("Description", TypeName = "text")]
        public string Description { get; set; }

        [Column("StartDate", TypeName ="timestamp")]
        public DateTime StartDate { get; set;}
        [Column("FinishDate", TypeName = "timestamp")]
        public DateTime? FinishDate { get; set;}

        [Column("Status", TypeName = "varchar(20)")]
        public Status Status { get; set; }

        public Project(string name, string description) {
            Name = name;
            Description = description;
            StartDate = DateTime.Now;
            FinishDate = null;
            Status = Status.NEW;
        }

        public Project() {
            Name = Description = string.Empty;
            StartDate = DateTime.Now;
            Status = Status.NEW;
        }
    }
}
