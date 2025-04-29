using ToDoApp.dto.create;
using ToDoApp.models;

namespace ToDoApp.factory
{
    public class TechnologyFactory
    {
        public TechnologyFactory()
        {

        }
        public Technology Build(CreateTechnologyDTO createTechnologyDTO)
        {
            return new Technology(createTechnologyDTO.Name);
        }
    }
}