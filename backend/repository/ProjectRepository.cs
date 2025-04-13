using ToDoApp.models.Project;
using ToDoApp.models.TodoItem;
using ToDoApp.repository.dbcontext.AppDbContext;
using ToDoApp.repository.interfaces.IProjectRepository;

namespace ToDoApp.repository
{
    namespace ProjectRepository
    {
        public class ProjectRepository : IProjectRepository
        {
            private readonly AppDbContext _context;
            public ProjectRepository(AppDbContext context)
            {
                _context = context;
            }
            public Task AddAsync(Project project)
            {
                throw new NotImplementedException();
            }

            public Task DeleteAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<List<TodoItem>> GetAllAsync()
            {
                throw new NotImplementedException();
            }

            public Task<TodoItem> GetByIdAsync()
            {
                throw new NotImplementedException();
            }

            public Task UpdateAsync(int id, Project project)
            {
                throw new NotImplementedException();
            }
        }
    }
}