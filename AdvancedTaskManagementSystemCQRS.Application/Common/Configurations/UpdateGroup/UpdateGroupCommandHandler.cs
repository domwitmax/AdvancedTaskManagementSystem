using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateGroupEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateGroup;

public class UpdateGroupCommandHandler(IMediator mediator) : IRequestHandler<UpdateGroupCommand, int?>
{
    public async Task<int?> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new UpdateGroupEntityCommand(request.OldName, request.NewName), cancellationToken);
    }
}
