using Microsoft.AspNetCore.Mvc;
using ToDoApp.models.TodoItem;
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
    }
}