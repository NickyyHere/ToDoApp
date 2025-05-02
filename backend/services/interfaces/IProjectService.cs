using ToDoApp.dto;
using ToDoApp.dto.create;

namespace ToDoApp.services.interfaces
{
    public interface IProjectService
    {
            Task<List<ProjectDTO>> GetProjectsAsync();
            Task<ProjectDTO> GetProjectAsync(int id);
            Task AddProjectAsync(CreateProjectDTO createProjectDTO);
            Task UpdateProjectAsync(CreateProjectDTO createProjectDTO, int id);
            Task DeleteProjectAsync(int id);
            Task ChangeProjectStatusAsync(int id);
            Task ImportAsync(ProjectDTO dto);
    }
}