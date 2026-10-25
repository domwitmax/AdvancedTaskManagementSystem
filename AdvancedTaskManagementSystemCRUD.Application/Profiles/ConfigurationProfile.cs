using AdvancedTaskManagementSystemCRUD.Application.Repository.Configurations.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configuration.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configurations.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Profiles
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            CreateMap<GroupDTO, GroupEntityDTO>();
            CreateMap<PriorityDTO, PriorityEntityDTO>();
            CreateMap<StatusDTO, StatusEntityDTO>();
            CreateMap<TypeDTO, TypeEntityDTO>();
            CreateMap<LabelDTO, LabelEntityDTO>();

            CreateMap<PriorityEntityDTO, PriorityDTO>();
            CreateMap<StatusEntityDTO, StatusDTO>();
            CreateMap<TypeEntityDTO, TypeDTO>();
            CreateMap<LabelEntity, LabelEntityDTO>();
            CreateMap<LabelEntityDTO, LabelDTO>();
            CreateMap<LabelEntityDTO, LabelEntity>();

            CreateMap<GroupEntityDTO, GroupEntity>();
            CreateMap<PriorityEntityDTO, PriorityEntity>();
            CreateMap<StatusEntityDTO, StatusEntity>();
            CreateMap<TypeEntityDTO, TypeEntity>();

            CreateMap<GroupEntity, GroupEntityDTO>();
            CreateMap<PriorityEntity, PriorityEntityDTO>();
            CreateMap<StatusEntity, StatusEntityDTO>();
            CreateMap<TypeEntity, TypeEntityDTO>();

            CreateMap<ProjectStatusEntity, ProjectStatusEntityDTO>();
            CreateMap<ProjectStatusEntityDTO, ProjectStatusEntity>();
            CreateMap<ProjectStatusDTO, ProjectStatusEntityDTO>();
            CreateMap<ProjectStatusEntityDTO, ProjectStatusDTO>();
        }
    }
}
