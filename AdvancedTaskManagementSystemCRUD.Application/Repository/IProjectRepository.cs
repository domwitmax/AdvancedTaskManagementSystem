using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Repository;

public interface IProjectRepository
{
    IEnumerable<ProjectEntityDTO> GetAllProjectsAsync();
    ProjectEntityDTO? GetProjectByIdAsync(int id);
    IEnumerable<ProjectEntityDTO> GetProjectsByUser(string userName);
    List<ProjectMemberEntityDTO> GetProjectMembers(int projectId);
    Task<int> AddProjectAsync(ProjectEntityDTO project, string createdBy);
    Task<int> UpdateProjectAsync(ProjectEntityDTO project);
    Task<int> DeleteProjectAsync(int id);
    Task<bool> AddProjectMember(int projectId, string userLogin, string addedBy);
    Task<bool> DeleteProjectMember(int projectId, string userLogin);
    bool IsProjectMember(int projectId, string userLogin);
}
