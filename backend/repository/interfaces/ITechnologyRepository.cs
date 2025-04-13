using ToDoApp.models.Project;
using ToDoApp.models.Technology;
using ToDoApp.models.TodoItem;

namespace ToDoApp.repository.interfaces
{
    namespace ITechnologyRepository
    {
        public interface ITechnologyRepository
        {
            Task<List<TodoItem>> GetAllAsync();
            Task<TodoItem> GetByIdAsync();
            Task AddAsync(Technology technology);
            Task UpdateAsync(int id, Technology technology);
            Task DeleteAsync(int id);
        }
    }
}