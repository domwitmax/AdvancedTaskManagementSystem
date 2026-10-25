using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddGroup;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddLabel;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddPriority;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddProjectStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateProjectStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddType;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateGroup;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateLabel;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdatePriority;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateType;
using AdvancedTaskManagementSystemCQRS.WebApi.Request;
using AutoMapper;

namespace AdvancedTaskManagementSystemCQRS.WebApi.Configurations;

public class ConfigurationProfile : Profile
{
    public ConfigurationProfile()
    {
        CreateMap<AddGroupRequest, AddGroupCommand>();
        CreateMap<AddPriorityRequest, AddPriorityCommand>();
        CreateMap<AddTypeRequest, AddTypeCommand>();
        CreateMap<AddStatusRequest, AddStatusCommand>();
        CreateMap<AddProjectStatusRequest, AddProjectStatusCommand>();
        CreateMap<AddLabelRequest, AddLabelCommand>();

        CreateMap<UpdateGroupRequest, UpdateGroupCommand>();
        CreateMap<UpdatePriorityRequest, UpdatePriorityCommand>();
        CreateMap<UpdateStatusRequest, UpdateStatusCommand>();
        CreateMap<UpdateProjectStatusRequest, UpdateProjectStatusCommand>();
        CreateMap<UpdateTypeRequest, UpdateTypeCommand>();
        CreateMap<UpdateLabelRequest, UpdateLabelCommand>();
    }
}
