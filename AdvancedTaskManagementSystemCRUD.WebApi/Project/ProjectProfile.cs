using AdvancedTaskManagementSystemCRUD.Application.Services.Project.DTOs;
using AdvancedTaskManagementSystemCRUD.WebApi.Request;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Project
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<CreateProjectRequest, ProjectDTO>();
            CreateMap<UpdateProjectRequest, ProjectDTO>();
            CreateMap<AddProjectMemberRequest, AddProjectMemberDTO>()
                .ForMember(dest => dest.AddedBy, opt => opt.Ignore());
        }
    }
}
