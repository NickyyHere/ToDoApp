using ToDoApp.dto;
using ToDoApp.dto.create;
using ToDoApp.mapper.interfaces;
using ToDoApp.models;

namespace ToDoApp.mapper
{
    public class TechnologyMapper : IMapper<Technology, TechnologyDTO>
    {
        public TechnologyDTO ToDTO(Technology model)
        {
            return new TechnologyDTO(model.Id, model.Name);
        }
        public List<TechnologyDTO> ToDTO(List<Technology> models)
        {
            List<TechnologyDTO> technologyDTOs = new();
            foreach(Technology model in models)
            {
                technologyDTOs.Add(ToDTO(model));
            }
            return technologyDTOs;
        }
    }
}