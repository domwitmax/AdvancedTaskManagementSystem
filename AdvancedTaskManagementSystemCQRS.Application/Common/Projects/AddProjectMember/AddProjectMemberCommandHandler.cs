using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.AddProjectMemberEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.AddProjectMember;

public class AddProjectMemberCommandHandler(IMediator mediator) : IRequestHandler<AddProjectMemberCommand, bool>
{
    public async Task<bool> Handle(AddProjectMemberCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new AddProjectMemberEntityCommand(request.ProjectId, request.UserLogin, request.AddedBy), cancellationToken);
    }
}
