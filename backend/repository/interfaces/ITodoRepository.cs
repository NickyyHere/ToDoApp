using ToDoApp.models;

namespace ToDoApp.repository.interfaces
{
    public interface ITodoRepository
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem> GetByIdAsync(int id);
        Task AddAsync(TodoItem item);
        Task UpdateAsync(int id, TodoItem item);
        Task DeleteAsync(int id);
    }
}