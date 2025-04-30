using System.Threading.Tasks;
using ToDoApp.dto;
using ToDoApp.dto.create;
using ToDoApp.mapper.interfaces;
using ToDoApp.models;
using ToDoApp.repository;
using ToDoApp.repository.interfaces;

namespace ToDoApp.mapper
{
    public class TaskMapper : IMapper<TodoItem, TodoItemDTO>
    {
        public TodoItemDTO ToDTO(TodoItem model)
        {
            return new TodoItemDTO(model.Id, model.Project.Id, model.Project.Name, model.Name, model.Description, model.Status, model.Technologies.Select(t => t.Technology.Name).ToList(), model.StartDate, model.FinishDate);
        }
        public List<TodoItemDTO> ToDTO(List<TodoItem> models)
        {
            List<TodoItemDTO> todoItemDTOs = new();
            foreach(TodoItem model in models)
            {
                todoItemDTOs.Add(ToDTO(model));
            }
            return todoItemDTOs;
        }
    }
}