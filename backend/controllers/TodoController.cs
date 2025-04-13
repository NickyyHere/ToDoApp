using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.dto.ProjectDTO;
using ToDoApp.dto.TodoItemDTO;
using ToDoApp.services.interfaces.ITodoService;

namespace ToDoApp.controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        [HttpGet("tasks")]
        public async Task<IActionResult> GetItems() 
        {
            var items = await _todoService.GetTodoItemsAsync();
            return Ok(items);
        }
        [HttpGet("projects")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _todoService.GetProjectsAsync();
            return Ok(projects);
        }
        [HttpPost("tasks")]
        public IActionResult AddTask([FromBody] TodoItemWithProjectIdDTO data)
        {
            int projectId = data.ProjectId;
            TodoItemDTO item = data.Item;
            _todoService.AddTodoItemAsync(item, projectId);
            return Ok();
        }
        [HttpPost("projects")]
        public async Task<IActionResult> AddProject([FromBody] ProjectDTO projectDTO)
        {
            await _todoService.AddProjectAsync(projectDTO);
            return Ok();
        }
    }
}