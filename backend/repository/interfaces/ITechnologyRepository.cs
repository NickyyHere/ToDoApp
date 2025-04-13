using ToDoApp.models.Technology;

namespace ToDoApp.repository.interfaces
{
    namespace ITechnologyRepository
    {
        public interface ITechnologyRepository
        {
            Task<List<Technology>> GetAllAsync();
            Task<Technology?> GetByIdAsync(int id);
            Task AddAsync(Technology technology);
            Task UpdateAsync(int id, Technology technology);
            Task DeleteAsync(int id);
        }
    }
}