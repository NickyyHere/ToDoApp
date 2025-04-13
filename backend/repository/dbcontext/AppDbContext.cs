using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ToDoApp.models.Project;
using ToDoApp.models.Technology;
using ToDoApp.models.TodoItem;

namespace ToDoApp.repository.dbcontext
{
    namespace AppDbContext
    {
        public class AppDbContext : DbContext
        {
            public DbSet<TodoItem> TodoItems { get; set; }
            public DbSet<Project> Projects { get; set; }
            public DbSet<Technology> Technologies { get; set; }

            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        }
    }
}
