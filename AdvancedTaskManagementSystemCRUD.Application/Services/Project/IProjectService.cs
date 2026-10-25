using AdvancedTaskManagementSystemCRUD.Application.Services.Project.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Project;

public interface IProjectService
{
    Task<int> CreateProject(ProjectDTO createProjectDTO, string createdBy);
    Task<int> UpdateProject(ProjectDTO updateProjectDTO);
    Task<int> DeleteProject(int projectId);
    ProjectDTO? GetProjectById(int projectId);
    List<ProjectDTO> GetAllProjects();
    List<ProjectDTO> GetProjectsByUser(string userName);
    List<ProjectMemberDTO> GetProjectMembers(int projectId);
    Task<bool> AddProjectMember(AddProjectMemberDTO addProjectMemberDTO);
    Task<bool> DeleteProjectMember(int projectId, string userLogin);
    bool CanAccessProject(int projectId, string userLogin);
}
