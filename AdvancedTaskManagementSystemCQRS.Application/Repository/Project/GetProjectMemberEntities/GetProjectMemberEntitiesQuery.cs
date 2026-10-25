using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetProjectMemberEntities;

public record GetProjectMemberEntitiesQuery(int ProjectId) : IRequest<List<ProjectMemberEntityDTO>>;
