using ToDoApp.dto;

namespace ToDoApp.mapper.interfaces
{
    public interface IMapper<MODEL, DTO>
    {
        public DTO ToDTO(MODEL model);
        public List<DTO> ToDTO(List<MODEL> models);
    }
}