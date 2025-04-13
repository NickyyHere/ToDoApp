using ToDoApp.models.Technology;
using ToDoApp.models.TodoItem;
using ToDoApp.repository.dbcontext.AppDbContext;
using ToDoApp.repository.interfaces.ITechnologyRepository;

namespace ToDoApp.repository
{
    namespace TechnologyRepository
    {
        public class TechnologyRepository : ITechnologyRepository
        {
            private readonly AppDbContext _context;
            public TechnologyRepository(AppDbContext context)
            {
                _context = context;
            }
            public Task AddAsync(Technology technology)
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

            public Task UpdateAsync(int id, Technology technology)
            {
                throw new NotImplementedException();
            }
        }
    }
}