using AdvancedTaskManagementSystemCRUD.Application.Repository.Configurations.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Repository
{
    public interface IConfigurationRepository
    {
        Task<int> AddGroup(GroupEntityDTO groupEntity);
        Task<int> AddPriority(PriorityEntityDTO priorityEntity);
        Task<int> AddStatus(StatusEntityDTO statusEntity);
        Task<int> AddProjectStatus(ProjectStatusEntityDTO projectStatusEntity);
        Task<int> AddType(TypeEntityDTO typeEntity);
        Task<int> AddLabel(LabelEntityDTO labelEntity);
        Task<int?> UpdateGroup(string oldName, string newName);
        Task<int?> UpdatePriority(PriorityEntityDTO priorityEntity);
        Task<int?> UpdateStatus(StatusEntityDTO statusEntity);
        Task<int?> UpdateProjectStatus(ProjectStatusEntityDTO projectStatusEntity);
        Task<int?> UpdateType(TypeEntityDTO typeEntity);
        Task<int?> UpdateLabel(LabelEntityDTO labelEntity);
        List<string> GetGroups();
        List<PriorityEntityDTO> GetPriorities();
        List<StatusEntityDTO> GetStatusByTypeId(int typeId);
        List<ProjectStatusEntityDTO> GetProjectStatuses();
        List<TypeEntityDTO> GetTypes();
        List<LabelEntityDTO> GetLabels();
        Task<bool> DeleteGroup(string name);
        Task<bool> DeletePriority(int id);
        Task<bool> DeleteStatus(int id);
        Task<bool> DeleteProjectStatus(int id);
        Task<bool> DeleteType(int id);
        Task<bool> DeleteLabel(int id);
    }
}
