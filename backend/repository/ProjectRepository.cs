using Microsoft.EntityFrameworkCore;
using ToDoApp.models;
using ToDoApp.repository.dbcontext;
using ToDoApp.repository.interfaces;

namespace ToDoApp.repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if(project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task UpdateAsync(int id, Project newProject)
        {
            var item = await _context.Projects.FindAsync(id);
            if(item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(newProject);
                await _context.SaveChangesAsync();
            }
        }
    }
}