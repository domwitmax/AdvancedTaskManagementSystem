using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DeleteProjectMemberEntity;

public record DeleteProjectMemberEntityCommand(int ProjectId, string UserLogin) : IRequest<bool>;
