using Microsoft.EntityFrameworkCore;
using Npgsql;
using ToDoApp.exception;
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
            try
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) when (e.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                throw new DuplicateEntityException($"Project {project.Name} already exists");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id) ?? throw new ItemNotFoundException($"Project with id {id} doesn't exist");
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id) ?? throw new ItemNotFoundException($"Project with id {id} doesn't exist");
        }

        public async Task UpdateAsync(int id, Project newProject)
        {
            var item = await _context.Projects.FindAsync(id) ?? throw new ItemNotFoundException($"Project with id {id} doesn't exist");;
            _context.Entry(item).CurrentValues.SetValues(newProject);
            await _context.SaveChangesAsync();
        }

        public async Task<Project> GetByNameAsync(string name)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.Name == name) ?? throw new ItemNotFoundException($"Project with name {name} doesn't exist");
        }
    }
}