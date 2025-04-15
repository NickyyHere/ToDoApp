using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using ToDoApp.dto;
using ToDoApp.models;
using ToDoApp.repository.interfaces;
using ToDoApp.services.interfaces.ITodoService;

namespace ToDoApp.services 
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IProjectRepository _projectRepository;

        public TodoService(ITodoRepository todoRepository, ITechnologyRepository technologyRepository, IProjectRepository projectRepository)
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
            if (project == null)
                throw new Exception("Can't add task to a project that does not exist");

            List<string> names = itemDTO.Technologies.Select(t => t.Name).ToList();
            var technologies = await _technologyRepository.GetByNamesAsync(names);
            foreach(Technology t in technologies) {
                Console.WriteLine(t.Name);
            }
            List<TodoTechnology> todoTechnologies = technologies.Select(t => new TodoTechnology{Technology = t}).ToList();
            TodoItem item = new TodoItem(itemDTO.Name, itemDTO.Description, todoTechnologies, project);
            await _todoRepository.AddAsync(item);
        }

        public async Task AddTechnologyAsync(string name)
        {
            Technology technology = new Technology(name);
            await _technologyRepository.AddAsync(technology);
        }
        public async Task<List<ProjectDTO>> GetProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            List<ProjectDTO> projectDTOs = new();
            foreach(Project p in projects)
            {
                projectDTOs.Add(new ProjectDTO(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Status,
                    p.StartDate,
                    p.FinishDate
                ));
            }
            return projectDTOs;
        }

        public async Task<List<TodoItemDTO>> GetTodoItemsAsync()
        {
            var items = await _todoRepository.GetAllAsync();
            List<TodoItemDTO> itemDTOs = new();
            foreach(TodoItem item in items)
            {
                itemDTOs.Add(new TodoItemDTO(
                    item.Project.Id,
                    item.Project.Name,
                    item.Name,
                    item.Description,
                    item.Status,
                    item.Technologies.Select(tt => tt.Technology).ToList(),
                    item.StartDate,
                    item.FinishDate
                ));
            }
            return itemDTOs;
        }
        public async Task<List<Technology>> GetTechnologiesAsync()
        {
            var technologies = await _technologyRepository.GetAllAsync();
            return technologies;
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
