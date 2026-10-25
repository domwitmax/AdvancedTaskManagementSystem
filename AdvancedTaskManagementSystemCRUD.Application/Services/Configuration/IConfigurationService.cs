using AdvancedTaskManagementSystemCRUD.Application.Services.Configuration.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configurations.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Configuration
{
    public interface IConfigurationService
    {
        Task<int> AddGroup(GroupDTO groupEntity);
        Task<int> AddPriority(PriorityDTO priorityEntity);
        Task<int> AddStatus(StatusDTO statusEntity);
        Task<int> AddProjectStatus(ProjectStatusDTO projectStatusEntity);
        Task<int> AddType(TypeDTO typeEntity);
        Task<int> AddLabel(LabelDTO labelEntity);
        Task<int?> UpdateGroup(string oldName, string newName);
        Task<int?> UpdatePriority(PriorityDTO priorityEntity);
        Task<int?> UpdateStatus(StatusDTO statusEntity);
        Task<int?> UpdateProjectStatus(ProjectStatusDTO projectStatusEntity);
        Task<int?> UpdateType(TypeDTO typeEntity);
        Task<int?> UpdateLabel(LabelDTO labelEntity);
        List<string> GetGroups();
        List<PriorityDTO> GetPriorities();
        List<StatusDTO> GetStatusByTypeId(int typeId);
        List<ProjectStatusDTO> GetProjectStatuses();
        List<TypeDTO> GetTypes();
        List<LabelDTO> GetLabels();
        Task<bool> DeleteGroup(string name);
        Task<bool> DeletePriority(int id);
        Task<bool> DeleteStatus(int id);
        Task<bool> DeleteProjectStatus(int id);
        Task<bool> DeleteType(int id);
        Task<bool> DeleteLabel(int id);
    }
}
