using ToDoApp.dto;
using ToDoApp.dto.create;
using ToDoApp.models;

namespace ToDoApp.services.interfaces
{
    namespace ITodoService
    {
        public interface ITodoService
        {
            Task<List<TodoItemDTO>> GetTodoItemsAsync();
            Task AddTodoItemAsync(CreateTodoItemDTO itemDTO);            
            Task UpdateTaskAsync(CreateTodoItemDTO createTodoItemDTO, int id);
            Task DeleteTaskAsync(int id);
            Task ChangeTaskStatusAsync(int id);
        }
    }  
}