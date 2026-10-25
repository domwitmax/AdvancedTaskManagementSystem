using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.ChangeRole;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.ChangeRole;

public class ChangeRoleCommandHandler(IMediator mediator) : IRequestHandler<ChangeRoleCommand, bool>
{
    public async Task<bool> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new ChangeRoleEntityCommand(request.Login, request.RoleId), cancellationToken);
    }
}
