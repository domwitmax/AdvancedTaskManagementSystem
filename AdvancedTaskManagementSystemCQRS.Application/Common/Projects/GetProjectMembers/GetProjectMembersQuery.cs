using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectMembers;

public record GetProjectMembersQuery(int ProjectId) : IRequest<List<ProjectMemberDTO>>;
