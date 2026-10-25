using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CreateProject;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.UpdateProject;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.CreateProjectEntity;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.UpdateProjectEntity;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;

namespace AdvancedTaskManagementSystemCQRS.Application.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectEntity, ProjectEntityDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name));
        CreateMap<ProjectEntityDTO, ProjectDTO>()
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status));
        CreateMap<CreateProjectCommand, CreateProjectEntityCommand>();
        CreateMap<CreateProjectEntityCommand, ProjectEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectMembers, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore());
        CreateMap<UpdateProjectCommand, UpdateProjectEntityCommand>();
        CreateMap<UpdateProjectEntityCommand, ProjectEntity>()
            .ForMember(dest => dest.ProjectMembers, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore());
        CreateMap<ProjectMemberEntity, ProjectMemberEntityDTO>()
            .ForMember(dest => dest.UserLogin, opt => opt.MapFrom(src => src.AccountLogin))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Account.RoleId))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Account.Role.Name));
        CreateMap<ProjectMemberEntityDTO, ProjectMemberDTO>();
    }
}
