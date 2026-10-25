using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddPriority;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddGroupEntity;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddProjectStatusEntity;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddPriorityEntity;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddLabelEntity;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddStatusEntity;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddTypeEntity;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DTOs;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;

namespace AdvancedTaskManagementSystemCQRS.Application.Profiles
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            CreateMap<AddGroupEntityCommand, GroupEntity>();
            CreateMap<AddProjectStatusEntityCommand, ProjectStatusEntity>();
            CreateMap<AddLabelEntityCommand, LabelEntity>();
            CreateMap<AddPriorityEntityCommand, PriorityEntity>();
            CreateMap<AddPriorityCommand, PriorityEntity>();
            CreateMap<AddStatusEntityCommand, StatusEntity>();
            CreateMap<AddTypeEntityCommand, TypeEntity>();

            CreateMap<PriorityEntity, PriorityEntityDTO>();
            CreateMap<StatusEntity, StatusEntityDTO>();
            CreateMap<TypeEntity, TypeEntityDTO>();
            CreateMap<ProjectStatusEntity, ProjectStatusEntityDTO>();

            CreateMap<PriorityEntityDTO, PriorityDTO>();
            CreateMap<StatusEntityDTO, StatusDTO>();
            CreateMap<TypeEntityDTO, TypeDTO>();
            CreateMap<ProjectStatusEntityDTO, ProjectStatusDTO>();
            CreateMap<LabelEntity, LabelEntityDTO>();
            CreateMap<LabelEntityDTO, LabelDTO>();
            CreateMap<LabelDTO, LabelEntityDTO>();
        }
    }
}
