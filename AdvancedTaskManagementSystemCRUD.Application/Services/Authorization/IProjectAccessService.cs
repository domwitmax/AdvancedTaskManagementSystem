using AdvancedTaskManagementSystemCRUD.Application.Services.Project.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Authorization;

public interface IProjectAccessService
{
    bool CanAccessProject(int projectId, string? userLogin, bool isAdministrator);
    bool CanViewUserProjects(string? currentUserLogin, string requestedUserLogin, bool isAdministrator);
    bool CanModifyComment(string commentCreatedBy, string? currentUserLogin, bool isAdministrator);
    List<ProjectDTO> GetAccessibleProjects(string? userLogin, bool isAdministrator);
    List<TaskDTO> FilterAccessibleTasks(List<TaskDTO> tasks, string? userLogin, bool isAdministrator);
}
