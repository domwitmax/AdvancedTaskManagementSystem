using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.AddProjectMember;

public record AddProjectMemberCommand(int ProjectId, string UserLogin, string AddedBy) : IRequest<bool>;
