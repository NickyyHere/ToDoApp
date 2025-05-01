using ToDoApp.dto;
using ToDoApp.dto.create;
using ToDoApp.enumerable;
using ToDoApp.factory;
using ToDoApp.mapper;
using ToDoApp.models;
using ToDoApp.repository.interfaces;
using ToDoApp.services.interfaces.ITodoService;

namespace ToDoApp.services 
{
    public class TodoService : ITodoService
    {
        private readonly TaskMapper _taskMapper = new();

        private readonly ITodoRepository _todoRepository;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly TodoItemFactory _taskFactory;
        public TodoService(ITodoRepository todoRepository, ITechnologyRepository technologyRepository, IProjectRepository projectRepository, TodoItemFactory taskFactory)
        {
            _todoRepository = todoRepository;
            _technologyRepository = technologyRepository;
            _projectRepository = projectRepository;
            _taskFactory = taskFactory;
        }
        public async Task AddTodoItemAsync(CreateTodoItemDTO createTodoItemDTO)
        {
            await _todoRepository.AddAsync(await _taskFactory.BuildAsync(createTodoItemDTO));
        }
        public async Task<List<TodoItemDTO>> GetTodoItemsAsync()
        {
            var items = await _todoRepository.GetAllAsync();
            return _taskMapper.ToDTO(items);
        }
        public async Task UpdateTaskAsync(CreateTodoItemDTO createTodoItemDTO, int id)
        {
            var todoItem = await _todoRepository.GetByIdAsync(id);
            var technologies = await _technologyRepository.GetByNamesAsync(createTodoItemDTO.TechnologyNames);
            if (todoItem.Project.Id != createTodoItemDTO.ProjectId) {
                var project = await _projectRepository.GetByIdAsync(createTodoItemDTO.ProjectId);
                todoItem.Project = project;
            }
            todoItem.Name = createTodoItemDTO.Name;
            todoItem.Description = createTodoItemDTO.Description;
            todoItem.Technologies = technologies.Select(t => new TodoTechnology{Technology = t}).ToList();
            await _todoRepository.UpdateAsync(id, todoItem);
        }
        public async Task DeleteTaskAsync(int id)
        {
            await _todoRepository.DeleteAsync(id);
        }
        public async Task ChangeTaskStatusAsync(int id)
        {
            await _todoRepository.ChangeStatusAsync(id);
        }

        public async Task ImportAsync(TodoItemDTO dto)
        {
            await _todoRepository.AddAsync(await _taskFactory.BuildAsync(dto));
        }
    }
}
