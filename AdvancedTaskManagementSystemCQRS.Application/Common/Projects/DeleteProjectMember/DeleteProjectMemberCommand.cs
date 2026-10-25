using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DeleteProjectMember;

public record DeleteProjectMemberCommand(int ProjectId, string UserLogin) : IRequest<bool>;
