using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ToDoApp.models;

namespace ToDoApp.repository.dbcontext
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<TodoTechnology> TodoTechnologies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoTechnology>()
                .HasKey(it => new { it.ItemId, it.TechnologyId });

            modelBuilder.Entity<TodoTechnology>()
                .HasOne(it => it.Item)
                .WithMany(i => i.Technologies)
                .HasForeignKey(it => it.ItemId);

            modelBuilder.Entity<TodoTechnology>()
                .HasOne(it => it.Technology)
                .WithMany(t => t.TodoTechnologies)
                .HasForeignKey(it => it.TechnologyId);
        }
    }
}
