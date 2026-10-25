using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteStatus;

public class DeleteStatusCommandHandler(IMediator mediator) : IRequestHandler<DeleteStatusCommand, bool>
{
    public async Task<bool> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new Repository.Configurations.DeleteStatus.DeleteStatusEntityCommand(request.Id), cancellationToken);
    }
}
