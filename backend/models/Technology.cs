using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.models
{
    namespace Technology
    {
        [Table("Technologies")]
        public class Technology
        {
            [Key]
            public int Id { get; set; }
            [Column("Name", TypeName = "varchar(50)")]
            public string Name { get; set; }

            public Technology(string name)
            {
                Name = name;
            }

            public Technology()
            {
                Name = string.Empty;
            }
        }
    }
}