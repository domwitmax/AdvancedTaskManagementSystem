using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Project.DTOs;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Project;

public class ProjectService(IProjectRepository projectRepository, IMapper mapper) : IProjectService
{
    public async Task<int> CreateProject(ProjectDTO createProjectDTO, string createdBy)
    {
        var projectEntityDTO = mapper.Map<ProjectEntityDTO>(createProjectDTO);
        return await projectRepository.AddProjectAsync(projectEntityDTO, createdBy);
    }

    public Task<int> DeleteProject(int projectId)
        => projectRepository.DeleteProjectAsync(projectId);

    public List<ProjectDTO> GetAllProjects()
        => mapper.Map<List<ProjectDTO>>(projectRepository.GetAllProjectsAsync());

    public List<ProjectDTO> GetProjectsByUser(string userName)
        => mapper.Map<List<ProjectDTO>>(projectRepository.GetProjectsByUser(userName));

    public ProjectDTO? GetProjectById(int projectId)
        => mapper.Map<ProjectDTO>(projectRepository.GetProjectByIdAsync(projectId));

    public Task<int> UpdateProject(ProjectDTO updateProjectDTO)
        => projectRepository.UpdateProjectAsync(mapper.Map<ProjectEntityDTO>(updateProjectDTO));

    public List<ProjectMemberDTO> GetProjectMembers(int projectId)
        => mapper.Map<List<ProjectMemberDTO>>(projectRepository.GetProjectMembers(projectId));

    public Task<bool> AddProjectMember(AddProjectMemberDTO addProjectMemberDTO)
        => projectRepository.AddProjectMember(
            addProjectMemberDTO.ProjectId,
            addProjectMemberDTO.UserLogin,
            addProjectMemberDTO.AddedBy);

    public Task<bool> DeleteProjectMember(int projectId, string userLogin)
        => projectRepository.DeleteProjectMember(projectId, userLogin);

    public bool CanAccessProject(int projectId, string userLogin)
        => projectRepository.IsProjectMember(projectId, userLogin);
}
