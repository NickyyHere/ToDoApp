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
    }
}