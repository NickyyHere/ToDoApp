using ToDoApp.dto.ProjectDTO;
using ToDoApp.dto.TodoItemDTO;
using ToDoApp.enumerable.Status;
using ToDoApp.models.Technology;
using ToDoApp.services.interfaces.ITodoService;

namespace ToDoApp.services 
{
    namespace TodoService
    {
        public class TodoService : ITodoService
        {
            public Task AddProjectAsync(ProjectDTO projectDTO)
            {
                throw new NotImplementedException();
            }

            public Task AddTodoItemAsync(TodoItemDTO itemDTO)
            {
                throw new NotImplementedException();
            }

            public async Task<List<object>> GetProjectsAsync()
            {
                await Task.Delay(100);
                List<object> projects = new();
                ProjectDTO project1 = new("NEW Title", "NEW Description", Status.NEW, DateTime.Now);
                ProjectDTO project2 = new("PROGRESS Title", "PROGRESS Description", Status.PROGRESS, DateTime.Now);
                ProjectDTO project3 = new("FINISHED Title", "FINISHED Description", Status.FINISHED, DateTime.Now);
                projects.Add(new {project = project1, technologies = new List<Technology>{new Technology("Vue"), new Technology(".NET")}});
                projects.Add(new {project = project2, technologies = new List<Technology>{new Technology(".NET"), new Technology("Vue")}});
                projects.Add(new {project = project3, technologies = new List<Technology>{new Technology("I wanna"), new Technology("Kill myself")}});
                return projects;
            }

            public async Task<List<TodoItemDTO>> GetTodoItemsAsync()
            {
                await Task.Delay(100);
                List<TodoItemDTO> items = [];
                List<Technology> techs = new List<Technology>{new Technology(".NET"), new Technology("Vue")};
                TodoItemDTO item1 = new("test1", "NEW Title", "NEW Description", Status.NEW, techs, DateTime.Now);
                TodoItemDTO item2 = new("test2", "PROGRESS Title", "PROGRESS Description", Status.PROGRESS, techs, DateTime.Now);
                TodoItemDTO item3 = new("test3", "FINISHED Title", "FINISHED Description", Status.FINISHED, techs, DateTime.Now);
                TodoItemDTO item4 = new("test3", "FINISHED Title", "FINISHED Description", Status.FINISHED, techs, DateTime.Now);
                items.Add(item1);
                items.Add(item2);
                items.Add(item3);
                items.Add(item4);
                return items;
            }
        }
    }
}
