using AdvancedTaskManagementSystemCRUD.Application.Services.Configuration.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configurations.DTOs;
using AdvancedTaskManagementSystemCRUD.WebApi.Request;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Configurations;

public class ConfigurationProfile : Profile
{
    public ConfigurationProfile()
    {
        CreateMap<AddGroupRequest, GroupDTO>();
        CreateMap<AddPriorityRequest, PriorityDTO>();
        CreateMap<AddTypeRequest, TypeDTO>();
        CreateMap<AddStatusRequest, StatusDTO>();

        CreateMap<UpdatePriorityRequest, PriorityDTO>();
        CreateMap<UpdateStatusRequest, StatusDTO>();
        CreateMap<UpdateTypeRequest, TypeDTO>();

        CreateMap<AddProjectStatusRequest, ProjectStatusDTO>();
        CreateMap<UpdateProjectStatusRequest, ProjectStatusDTO>();

        CreateMap<AddLabelRequest, LabelDTO>();
        CreateMap<UpdateLabelRequest, LabelDTO>();
    }
}
