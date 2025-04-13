using ToDoApp.models.Project;
using ToDoApp.models.TodoItem;

namespace ToDoApp.repository.interfaces
{
    namespace IProjectRepository
    {
        public interface IProjectRepository
        {
            Task<List<TodoItem>> GetAllAsync();
            Task<TodoItem> GetByIdAsync();
            Task AddAsync(Project project);
            Task UpdateAsync(int id, Project project);
            Task DeleteAsync(int id);
        }
    }
}