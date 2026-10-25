using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DeleteProjectMemberEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DeleteProjectMember;

public class DeleteProjectMemberCommandHandler(IMediator mediator) : IRequestHandler<DeleteProjectMemberCommand, bool>
{
    public async Task<bool> Handle(DeleteProjectMemberCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteProjectMemberEntityCommand(request.ProjectId, request.UserLogin), cancellationToken);
    }
}
