using ToDoApp.dto.create;
using ToDoApp.models;

namespace ToDoApp.factory
{
    public class ProjectFactory
    {
        public ProjectFactory()
        {

        }

        public Project Build(CreateProjectDTO createProjectDTO)
        {
            return new Project(createProjectDTO.Name, createProjectDTO.Description);
        }
    }
}