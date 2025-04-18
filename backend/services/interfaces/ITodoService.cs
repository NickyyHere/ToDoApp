using ToDoApp.dto;
using ToDoApp.models;

namespace ToDoApp.services.interfaces
{
    namespace ITodoService
    {
        public interface ITodoService
        {
            Task<List<TodoItemDTO>> GetTodoItemsAsync();
            Task AddTodoItemAsync(TodoItemDTO itemDTO, int projectId);
            Task<List<ProjectDTO>> GetProjectsAsync();
            Task AddProjectAsync(ProjectDTO projectDTO);
            Task AddTechnologyAsync(string name);
            Task<List<Technology>> GetTechnologiesAsync();
            Task UpdateProject(ProjectDTO projectDTO, int id);
            Task UpdateTask(TodoItemDTO todoItemDTO, int id);
            Task DeleteProject(int id);
            Task DeleteTask(int id);
            Task ChangeProjectStatus(int id);
            Task ChangeTaskStatus(int id);
        }
    }  
}