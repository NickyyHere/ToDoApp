using ToDoApp.dto.ProjectDTO;
using ToDoApp.dto.TodoItemDTO;
using ToDoApp.models.Technology;

namespace ToDoApp.services.interfaces
{
    namespace ITodoService
    {
        public interface ITodoService
        {
            Task<List<TodoItemDTO>> GetTodoItemsAsync();
            Task AddTodoItemAsync(TodoItemDTO itemDTO, int projectId);
            Task<List<object>> GetProjectsAsync();
            Task AddProjectAsync(ProjectDTO projectDTO);
            
        }
    }  
}