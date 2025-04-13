using ToDoApp.dto.ProjectDTO;
using ToDoApp.dto.TodoItemDTO;
using ToDoApp.models.Project;
using ToDoApp.models.Technology;
using ToDoApp.models.TodoItem;
using ToDoApp.repository.ProjectRepository;
using ToDoApp.repository.TechnologyRepository;
using ToDoApp.repository.TodoRepository;
using ToDoApp.services.interfaces.ITodoService;

namespace ToDoApp.services 
{
    namespace TodoService
    {
        public class TodoService : ITodoService
        {
            private readonly TodoRepository _todoRepository;
            private readonly TechnologyRepository _technologyRepository;
            private readonly ProjectRepository _projectRepository;

            public TodoService(TodoRepository todoRepository, TechnologyRepository technologyRepository, ProjectRepository projectRepository)
            {
                _todoRepository = todoRepository;
                _technologyRepository = technologyRepository;
                _projectRepository = projectRepository;
            }
            public async Task AddProjectAsync(ProjectDTO projectDTO)
            {
                Project newProject = new Project(projectDTO.Name, projectDTO.Description);
                await _projectRepository.AddAsync(newProject);
            }

            public async Task AddTodoItemAsync(TodoItemDTO itemDTO, int projectId)
            {
                Project? project = await _projectRepository.GetByIdAsync(projectId);
                if(project == null)
                {
                    throw new Exception("Can't add task to a project that does not exist");
                }
                TodoItem item = new TodoItem(itemDTO.Name, itemDTO.Description, itemDTO.Technologies.ToList(), project);
                await _todoRepository.AddAsync(item);
            }

            public async Task<List<object>> GetProjectsAsync()
            {
                var projects = await _projectRepository.GetAllAsync();
                List<object> projectsAndTechnologies = new();
                foreach(Project p in projects)
                {
                    HashSet<Technology> techs = new();
                    List<TodoItem> todoItems = await GetProjectTasksAsync(p);
                    foreach(TodoItem item in todoItems)
                    {
                        foreach(Technology tech in item.Technologies)
                        {
                            techs.Add(tech);
                        }
                    }
                    projectsAndTechnologies.Add(new {project = p, technologies = techs.ToList()});
                }
                return projectsAndTechnologies;
            }

            public async Task<List<TodoItemDTO>> GetTodoItemsAsync()
            {
                var items = await _todoRepository.GetAllAsync();
                List<TodoItemDTO> itemDTOs = new();
                foreach(TodoItem item in items)
                {
                    itemDTOs.Add(new TodoItemDTO(
                        item.Project.Name,
                        item.Name,
                        item.Description,
                        item.Status,
                        item.Technologies,
                        item.StartDate,
                        item.FinishDate
                    ));
                }
                return itemDTOs;
            }

            private async Task<List<TodoItem>> GetProjectTasksAsync(Project project)
            {
                List<TodoItem> tasks = await _todoRepository.GetAllAsync();
                List<TodoItem> projectTasks = new();
                foreach(TodoItem item in tasks)
                {
                    if(item.Project == project)
                    {
                        projectTasks.Add(item);
                    }
                }
                return projectTasks;
            }
        }
    }
}
