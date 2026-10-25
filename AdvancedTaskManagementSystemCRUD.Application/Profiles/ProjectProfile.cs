using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Project.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectEntityDTO, ProjectEntity>()
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectMembers, opt => opt.Ignore());
        CreateMap<ProjectEntity, ProjectEntityDTO>()
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));

        CreateMap<ProjectDTO, ProjectEntityDTO>();
        CreateMap<ProjectEntityDTO, ProjectDTO>();

        CreateMap<ProjectMemberEntity, ProjectMemberEntityDTO>()
            .ForMember(dest => dest.UserLogin, opt => opt.MapFrom(src => src.AccountLogin))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Account.RoleId))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Account.Role.Name));
        CreateMap<ProjectMemberEntityDTO, ProjectMemberDTO>();
    }
}
