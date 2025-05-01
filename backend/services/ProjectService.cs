using ToDoApp.dto;
using ToDoApp.dto.create;
using ToDoApp.enumerable;
using ToDoApp.exception;
using ToDoApp.factory;
using ToDoApp.mapper;
using ToDoApp.models;
using ToDoApp.repository.interfaces;
using ToDoApp.services.interfaces;

namespace ToDoApp.services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITodoRepository _todoRepository;
        private readonly ProjectMapper _projectMapper = new();
        private readonly ProjectFactory _projectFactory = new();
        public ProjectService(IProjectRepository projectRepository, ITodoRepository todoRepository)
        {
            _projectRepository = projectRepository;
            _todoRepository = todoRepository;
        }
        public async Task AddProjectAsync(CreateProjectDTO createProjectDTO)
        {
            await _projectRepository.AddAsync(_projectFactory.Build(createProjectDTO));
        }
        public async Task UpdateProjectAsync(CreateProjectDTO createProjectDTO, int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            project.Name = createProjectDTO.Name;
            project.Description = createProjectDTO.Description;
            await _projectRepository.UpdateAsync(id, project);
        }
        public async Task DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            var projectTasks = await GetProjectTasksAsync(project);
            foreach(TodoItem task in projectTasks)
            {
                await _todoRepository.DeleteAsync(task.Id);
            }
            await _projectRepository.DeleteAsync(id);
        }
        public async Task ChangeProjectStatusAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if ((int)project.Status++ > 2)
            {
                project.Status = Status.FINISHED;
                throw new ItemAlreadyFinishedException($"Project: {project.Name} is already finished");
            }
            if((int)project.Status == (int)Status.FINISHED)
                project.FinishDate = DateTime.Now;
            await _projectRepository.UpdateAsync(id, project);
        }
        public async Task<List<ProjectDTO>> GetProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _projectMapper.ToDTO(projects);
        }
        private async Task<List<TodoItem>> GetProjectTasksAsync(Project project)
        {
            var tasks = await _todoRepository.GetAllAsync();
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

        public async Task ImportAsync(ProjectDTO dto)
        {
            await _projectRepository.AddAsync(_projectFactory.Build(dto));
        }
    }
}