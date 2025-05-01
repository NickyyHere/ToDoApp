using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.dto;
using ToDoApp.dto.create;
using ToDoApp.exception;
using ToDoApp.services.interfaces;
using ToDoApp.services.interfaces.ITodoService;

namespace ToDoApp.controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IProjectService _projectService;
        private readonly ITechnologyService _technologyService;
        public TodoController(ITodoService todoService, IProjectService projectService, ITechnologyService technologyService)
        {
            _todoService = todoService;
            _projectService = projectService;
            _technologyService = technologyService;
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
            var projects = await _projectService.GetProjectsAsync();
            return Ok(projects);
        }
        [HttpGet("technologies")]
        public async Task<IActionResult> GetTechnologies()
        {
            var technologies = await _technologyService.GetTechnologiesAsync();
            return Ok(technologies);
        }
        [HttpPost("tasks")]
        public async Task<IActionResult> AddTask([FromBody] CreateTodoItemDTO data)
        {
            await _todoService.AddTodoItemAsync(data);
            return Ok();
        }
        [HttpPost("projects")]
        public async Task<IActionResult> AddProject([FromBody] CreateProjectDTO createProjectDTO)
        {
            await _projectService.AddProjectAsync(createProjectDTO);
            return Ok();
        }
        [HttpPost("technologies")]
        public async Task<IActionResult> AddTechnology([FromBody] CreateTechnologyDTO createTechnologyDTO)
        {
            await _technologyService.AddTechnologyAsync(createTechnologyDTO);
            return Ok();
        }
        [HttpPost("import/tasks")]
        public async Task<IActionResult> ImportTask([FromBody] TodoItemDTO data, [FromRoute] int status)
        {
            await _todoService.ImportAsync(data);
            return Ok();
        }
        [HttpPost("import/projects")]
        public async Task<IActionResult> ImportProject([FromBody] ProjectDTO data)
        {
            await _projectService.ImportAsync(data);
            return Ok();
        }
        [HttpPost("import/technologies")]
        public async Task<IActionResult> ImportTechnology([FromBody] TechnologyDTO data)
        {
            await _technologyService.ImportAsync(data);
            return Ok();
        }
        [HttpPut("projects/{id}")]
        public async Task<IActionResult> UpdateProject([FromBody] CreateProjectDTO createProjectDTO, [FromRoute] int id)
        {
            try
            {
                await _projectService.UpdateProjectAsync(createProjectDTO, id);
            }
            catch(ItemNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }
        [HttpPut("tasks/{id}")]
        public async Task<IActionResult> UpdateTask([FromBody] CreateTodoItemDTO createTodoItemDTO, [FromRoute] int id)
        {
            try
            {
                await _todoService.UpdateTaskAsync(createTodoItemDTO, id);
            }
            catch(ItemNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }
        [HttpDelete("tasks/{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            await _todoService.DeleteTaskAsync(id);
            return Ok();
        }
        [HttpDelete("projects/{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            try
            {
                await _projectService.DeleteProjectAsync(id);
            }
            catch(ItemNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }
        [HttpPut("projects/status/{id}")]
        public async Task<IActionResult> ChangeProjectStatus([FromRoute] int id)
        {
            try
            {
                await _projectService.ChangeProjectStatusAsync(id);
            }
            catch(ItemNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(ItemAlreadyFinishedException e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
        [HttpPut("tasks/status/{id}")]
        public async Task<IActionResult> ChangeTaskStatus([FromRoute] int id)
        {
            try
            {
                await _todoService.ChangeTaskStatusAsync(id);
            }
            catch(ItemNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(ItemAlreadyFinishedException e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}