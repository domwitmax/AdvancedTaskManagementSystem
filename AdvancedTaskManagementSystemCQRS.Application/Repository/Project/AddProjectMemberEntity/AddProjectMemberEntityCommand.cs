using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Project.AddProjectMemberEntity;

public record AddProjectMemberEntityCommand(int ProjectId, string UserLogin, string AddedBy) : IRequest<bool>;
