using ToDoApp.models;

namespace ToDoApp.repository.interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task AddAsync(Project project);
        Task UpdateAsync(int id, Project project);
        Task DeleteAsync(int id);
    }
}