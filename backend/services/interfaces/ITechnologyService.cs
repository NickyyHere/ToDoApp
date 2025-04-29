using ToDoApp.dto;
using ToDoApp.dto.create;

namespace ToDoApp.services.interfaces
{
    public interface ITechnologyService
    {
        Task AddTechnologyAsync(CreateTechnologyDTO createTechnologyDTO);
        Task<List<TechnologyDTO>> GetTechnologiesAsync();
    }
}