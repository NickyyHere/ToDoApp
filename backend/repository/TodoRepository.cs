using Microsoft.EntityFrameworkCore;
using ToDoApp.models;
using ToDoApp.repository.dbcontext;
using ToDoApp.repository.interfaces;

namespace ToDoApp.repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;
        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if(item != null) 
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task UpdateAsync(int id, TodoItem newItem)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if(item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(newItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}