using ToDoApp.dto;
using ToDoApp.dto.create;
using ToDoApp.mapper.interfaces;
using ToDoApp.models;

namespace ToDoApp.mapper
{
    public class ProjectMapper : IMapper<Project, ProjectDTO>
    {
        public ProjectMapper(){}
        public ProjectDTO ToDTO(Project model)
        {
            return new ProjectDTO(model.Id, model.Name, model.Description, model.Status, model.StartDate, model.FinishDate);
        }

        public List<ProjectDTO> ToDTO(List<Project> models)
        {
            List<ProjectDTO> projectDTOs = new();
            foreach(Project model in models)
            {
                projectDTOs.Add(ToDTO(model));
            }
            return projectDTOs;
        }
    }
}