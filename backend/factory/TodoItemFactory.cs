using ToDoApp.dto;
using ToDoApp.dto.create;
using ToDoApp.models;
using ToDoApp.repository.interfaces;

namespace ToDoApp.factory
{
    public class TodoItemFactory
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IProjectRepository _projectRepository;
        public TodoItemFactory(ITechnologyRepository technologyRepository, IProjectRepository projectRepository)
        {
            _technologyRepository = technologyRepository;
            _projectRepository = projectRepository;
        }

        public async Task<TodoItem> BuildAsync(CreateTodoItemDTO createTodoItemDTO)
        {
            var technologies = await _technologyRepository.GetByNamesAsync(createTodoItemDTO.TechnologyNames);
            var project = await _projectRepository.GetByIdAsync(createTodoItemDTO.ProjectId) ?? throw new Exception("Project does not exist");
            return new TodoItem(createTodoItemDTO.Name, createTodoItemDTO.Description, technologies.Select(t => new TodoTechnology{Technology = t}).ToList(), project);
        }
        public async Task<TodoItem> BuildAsync(TodoItemDTO todoItemDTO)
        {
            var technologies = await _technologyRepository.GetByNamesAsync(todoItemDTO.Technologies);
            var project = await _projectRepository.GetByNameAsync(todoItemDTO.ProjectName) ?? throw new Exception("Couldn't find project");
            return new TodoItem {
                Project = project,
                Name = todoItemDTO.Name,
                Description = todoItemDTO.Description,
                Technologies = technologies.Select(t => new TodoTechnology{Technology = t}).ToList(),
                StartDate = todoItemDTO.StartDate,
                FinishDate = todoItemDTO.FinishDate,
                Status = todoItemDTO.Status
            };
        }
    }
}