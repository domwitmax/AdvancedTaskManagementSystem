using AdvancedTaskManagementSystemCRUD.Application.Services.Project;
using AdvancedTaskManagementSystemCRUD.Application.Services.Project.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Authorization;

public class ProjectAccessService(IProjectService projectService) : IProjectAccessService
{
    public bool CanAccessProject(int projectId, string? userLogin, bool isAdministrator)
    {
        if (isAdministrator)
            return true;

        return !string.IsNullOrWhiteSpace(userLogin) &&
               projectService.CanAccessProject(projectId, userLogin);
    }

    public bool CanViewUserProjects(
        string? currentUserLogin,
        string requestedUserLogin,
        bool isAdministrator)
    {
        return isAdministrator ||
               (!string.IsNullOrWhiteSpace(currentUserLogin) &&
                currentUserLogin.Equals(requestedUserLogin, StringComparison.OrdinalIgnoreCase));
    }

    public bool CanModifyComment(
        string commentCreatedBy,
        string? currentUserLogin,
        bool isAdministrator)
    {
        return isAdministrator ||
               (!string.IsNullOrWhiteSpace(currentUserLogin) &&
                currentUserLogin.Equals(commentCreatedBy, StringComparison.OrdinalIgnoreCase));
    }

    public List<ProjectDTO> GetAccessibleProjects(string? userLogin, bool isAdministrator)
    {
        if (isAdministrator)
            return projectService.GetAllProjects();

        return string.IsNullOrWhiteSpace(userLogin)
            ? new List<ProjectDTO>()
            : projectService.GetProjectsByUser(userLogin);
    }

    public List<TaskDTO> FilterAccessibleTasks(
        List<TaskDTO> tasks,
        string? userLogin,
        bool isAdministrator)
    {
        if (isAdministrator)
            return tasks;

        if (string.IsNullOrWhiteSpace(userLogin))
            return new List<TaskDTO>();

        var projectIds = projectService
            .GetProjectsByUser(userLogin)
            .Select(project => project.Id)
            .ToHashSet();

        return tasks
            .Where(task => projectIds.Contains(task.ProjectId))
            .ToList();
    }
}
