using ToDoApp.dto;
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
        public Project Build(ProjectDTO projectDTO)
        {
            return new Project{
                Name = projectDTO.Name,
                Description = projectDTO.Description,
                StartDate = projectDTO.StartDate,
                FinishDate = projectDTO.FinishDate,
                Status = projectDTO.Status
            };
        }
    }
}