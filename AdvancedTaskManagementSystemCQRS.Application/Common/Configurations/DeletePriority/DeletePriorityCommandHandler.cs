using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeletePriority;

public class DeletePriorityCommandHandler(IMediator mediator) : IRequestHandler<DeletePriorityCommand, bool>
{
    public async Task<bool> Handle(DeletePriorityCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new Repository.Configurations.DeletePriority.DeletePriorityEntityCommand(request.Id), cancellationToken);
    }
}
