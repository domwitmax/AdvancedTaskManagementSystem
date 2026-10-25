using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CreateProject;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.UpdateProject;
using AdvancedTaskManagementSystemCQRS.WebApi.Projects.Request;
using AutoMapper;

namespace AdvancedTaskManagementSystemCQRS.WebApi.Projects;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<CreateProjectRequest, CreateProjectCommand>()
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
        CreateMap<UpdateProjectRequest, UpdateProjectCommand>();
    }
}
