using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.Repository;

public class ProjectRepository(TaskManagerDbContext dbContext, IMapper mapper) : IProjectRepository
{
    public async Task<int> AddProjectAsync(ProjectEntityDTO project, string createdBy)
    {
        if (!await dbContext.Account.AnyAsync(item => item.Login == createdBy && item.Active))
            return 0;

        var projectEntity = mapper.Map<ProjectEntity>(project);
        projectEntity.ProjectMembers.Add(new ProjectMemberEntity
        {
            AccountLogin = createdBy,
            AddedAt = DateTime.UtcNow,
            AddedBy = createdBy
        });

        dbContext.Projects.Add(projectEntity);
        await dbContext.SaveChangesAsync();
        return projectEntity.Id;
    }

    public async Task<int> DeleteProjectAsync(int id)
    {
        var project = await dbContext.Projects.FirstOrDefaultAsync(item => item.Id == id);
        if (project == null)
            return 0;
        if (await dbContext.Task.AnyAsync(item => item.ProjectId == id))
            return -1;

        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync();
        return 1;
    }

    public IEnumerable<ProjectEntityDTO> GetAllProjectsAsync()
    {
        var projects = dbContext.Projects
            .AsNoTracking()
            .Include(item => item.Status)
            .OrderBy(item => item.Name)
            .ToList();
        return mapper.Map<IEnumerable<ProjectEntityDTO>>(projects);
    }

    public IEnumerable<ProjectEntityDTO> GetProjectsByUser(string userName)
    {
        var projects = dbContext.Projects
            .AsNoTracking()
            .Include(item => item.Status)
            .Where(item => item.ProjectMembers.Any(member => member.AccountLogin == userName))
            .OrderBy(item => item.Name)
            .ToList();
        return mapper.Map<IEnumerable<ProjectEntityDTO>>(projects);
    }

    public ProjectEntityDTO? GetProjectByIdAsync(int id)
    {
        var project = dbContext.Projects
            .AsNoTracking()
            .Include(item => item.Status)
            .FirstOrDefault(item => item.Id == id);
        return mapper.Map<ProjectEntityDTO?>(project);
    }

    public List<ProjectMemberEntityDTO> GetProjectMembers(int projectId)
    {
        var members = dbContext.ProjectMembers
            .AsNoTracking()
            .Include(item => item.Account)
            .ThenInclude(item => item.Role)
            .Where(item => item.ProjectId == projectId)
            .OrderBy(item => item.AccountLogin)
            .ToList();
        return mapper.Map<List<ProjectMemberEntityDTO>>(members);
    }

    public async Task<bool> AddProjectMember(int projectId, string userLogin, string addedBy)
    {
        if (!await dbContext.Projects.AnyAsync(item => item.Id == projectId))
            return false;

        if (!await dbContext.Account.AnyAsync(item => item.Login == userLogin && item.Active))
            return false;

        if (await dbContext.ProjectMembers.AnyAsync(
            item => item.ProjectId == projectId && item.AccountLogin == userLogin))
            return false;

        dbContext.ProjectMembers.Add(new ProjectMemberEntity
        {
            ProjectId = projectId,
            AccountLogin = userLogin,
            AddedAt = DateTime.UtcNow,
            AddedBy = addedBy
        });
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteProjectMember(int projectId, string userLogin)
    {
        var member = await dbContext.ProjectMembers.FirstOrDefaultAsync(
            item => item.ProjectId == projectId && item.AccountLogin == userLogin);
        if (member == null)
            return false;

        if (await dbContext.Task.AnyAsync(
            item => item.ProjectId == projectId && item.AssignedTo == userLogin))
            return false;

        dbContext.ProjectMembers.Remove(member);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public bool IsProjectMember(int projectId, string userLogin)
    {
        return dbContext.ProjectMembers.Any(
            item => item.ProjectId == projectId && item.AccountLogin == userLogin);
    }

    public async Task<int> UpdateProjectAsync(ProjectEntityDTO project)
    {
        var projectEntity = dbContext.Projects.FirstOrDefault(item => item.Id == project.Id);
        if (projectEntity == null)
            return 0;

        mapper.Map(project, projectEntity);
        dbContext.Projects.Update(projectEntity);
        await dbContext.SaveChangesAsync();
        return projectEntity.Id;
    }
}
