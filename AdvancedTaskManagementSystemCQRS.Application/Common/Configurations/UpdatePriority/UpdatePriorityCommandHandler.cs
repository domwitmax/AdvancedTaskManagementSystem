using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdatePriorityEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdatePriority;

public class UpdatePriorityCommandHandler(IMediator mediator) : IRequestHandler<UpdatePriorityCommand, int?>
{
    public async Task<int?> Handle(UpdatePriorityCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new UpdatePriorityEntityCommand(request.Id, request.Name), cancellationToken);
    }
}
