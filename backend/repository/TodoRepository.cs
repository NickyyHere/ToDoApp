using ToDoApp.models.TodoItem;
using ToDoApp.repository.dbcontext.AppDbContext;
using ToDoApp.repository.interfaces.ITodoRepository;

namespace ToDoApp.repository
{
    namespace TodoRepository
    {
        public class TodoRepository : ITodoRepository
        {
            private readonly AppDbContext _context;
            public TodoRepository(AppDbContext context)
            {
                _context = context;
            }
            public Task AddAsync(TodoItem item)
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

            public Task UpdateAsync(int id, TodoItem item)
            {
                throw new NotImplementedException();
            }
        }
    }
}