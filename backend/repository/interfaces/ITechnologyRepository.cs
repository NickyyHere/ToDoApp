using ToDoApp.models;

namespace ToDoApp.repository.interfaces
{
    public interface ITechnologyRepository
    {
        Task<List<Technology>> GetAllAsync();
        Task<Technology?> GetByIdAsync(int id);
        Task AddAsync(Technology technology);
        Task UpdateAsync(int id, Technology technology);
        Task DeleteAsync(int id);
        Task<List<Technology>> GetByNamesAsync(IEnumerable<string> names);
    }
}