using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Project.IsProjectMember;

public record IsProjectMemberQuery(int ProjectId, string UserLogin) : IRequest<bool>;
