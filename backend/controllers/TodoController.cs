using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.dto;
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
        [HttpGet("technologies")]
        public async Task<IActionResult> GetTechnologiesAsync()
        {
            var technologies = await _todoService.GetTechnologiesAsync();
            return Ok(technologies);
        }
        [HttpPost("tasks/{id}")]
        public async Task<IActionResult> AddTask([FromBody] TodoItemDTO data, [FromRoute] int id)
        {
            int projectId = id;
            TodoItemDTO item = data;
            await _todoService.AddTodoItemAsync(item, projectId);
            return Ok();
        }
        [HttpPost("projects")]
        public async Task<IActionResult> AddProject([FromBody] ProjectDTO projectDTO)
        {
            await _todoService.AddProjectAsync(projectDTO);
            return Ok();
        }
        [HttpPost("technologies")]
        public async Task<IActionResult> AddTechnology([FromBody] string name)
        {
            await _todoService.AddTechnologyAsync(name);
            return Ok();
        }
        [HttpPut("projects/{id}")]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectDTO projectDTO, [FromRoute] int id)
        {
            await _todoService.UpdateProject(projectDTO, id);
            return Ok();
        }
        [HttpPut("tasks/{id}")]
        public async Task<IActionResult> UpdateTask([FromBody] TodoItemDTO todoItemDTO, [FromRoute] int id)
        {
            await _todoService.UpdateTask(todoItemDTO, id);
            return Ok();
        }
        [HttpDelete("tasks/{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            await _todoService.DeleteTask(id);
            return Ok();
        }
        [HttpDelete("projects/{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            await _todoService.DeleteProject(id);
            return Ok();
        }
        [HttpPut("projects/status/{id}")]
        public async Task<IActionResult> ChangeProjectStatus([FromRoute] int id)
        {
            await _todoService.ChangeProjectStatus(id);
            return Ok();
        }
        [HttpPut("tasks/status/{id}")]
        public async Task<IActionResult> ChangeTaskStatus([FromRoute] int id)
        {
            await _todoService.ChangeTaskStatus(id);
            return Ok();
        }
    }
}