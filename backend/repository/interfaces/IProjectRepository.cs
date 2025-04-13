using ToDoApp.models.Project;

namespace ToDoApp.repository.interfaces
{
    namespace IProjectRepository
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
}