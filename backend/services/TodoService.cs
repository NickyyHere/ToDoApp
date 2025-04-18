using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using ToDoApp.dto;
using ToDoApp.enumerable;
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

            List<string> names = itemDTO.Technologies.Select(t => t).ToList();
            var technologies = await _technologyRepository.GetByNamesAsync(names);
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
                    item.Id,
                    item.Project.Name,
                    item.Name,
                    item.Description,
                    item.Status,
                    item.Technologies.Select(tt => tt.Technology.Name).ToList(),
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
        public async Task UpdateProject(ProjectDTO projectDTO, int id)
        {
            Project? project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                throw new Exception("Project does not exist");
            
            project.Name = projectDTO.Name;
            project.Description = projectDTO.Description;
            await _projectRepository.UpdateAsync(id, project);
        }
        public async Task UpdateTask(TodoItemDTO todoItemDTO, int id)
        {
            TodoItem? todoItem = await _todoRepository.GetByIdAsync(id);
            if(todoItem == null)
                throw new Exception("Task does not exist");
            if(todoItemDTO.ProjectName != todoItem.Project.Name) {
                var project = await _projectRepository.GetByNameAsync(todoItemDTO.ProjectName);
                if(project == null)
                    throw new Exception("Couldn't find project");
                
                todoItem.Project = project;
            }
            var technologies = await _technologyRepository.GetByNamesAsync(todoItemDTO.Technologies);
            todoItem.Name = todoItemDTO.Name;
            todoItem.Description = todoItemDTO.Description;
            todoItem.Technologies = technologies.Select(t => new TodoTechnology{Technology = t}).ToList();
            await _todoRepository.UpdateAsync(id, todoItem);
        }
        public async Task DeleteTask(int id)
        {
            await _todoRepository.DeleteAsync(id);
        }
        public async Task DeleteProject(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if(project == null)
                throw new Exception("Project does not exist");
            
            List<TodoItem> projectTasks = await GetProjectTasksAsync(project);
            foreach(TodoItem task in projectTasks)
            {
                await _todoRepository.DeleteAsync(task.Id);
            }
            await _projectRepository.DeleteAsync(id);
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
        public async Task ChangeProjectStatus(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if(project == null)
                throw new Exception("Project does not exist");
            if((int)project.Status++ > 2)
                throw new Exception("Project is already of finished status");
            if((int)project.Status == (int)Status.FINISHED)
                project.FinishDate = DateTime.Now;
            await _projectRepository.UpdateAsync(id, project);
        }
        public async Task ChangeTaskStatus(int id)
        {
            var task = await _todoRepository.GetByIdAsync(id);
            if(task == null)
                throw new Exception("Project does not exist");
            if((int)task.Status++ > (int)Status.FINISHED)
                throw new Exception("Project is already of finished status");
            if((int)task.Status == (int)Status.FINISHED)
                task.FinishDate = DateTime.Now;
            await _todoRepository.UpdateAsync(id, task);
        }
    }
}
