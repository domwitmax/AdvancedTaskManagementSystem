using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateStatusEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateStatus;

public class UpdateStatusCommandHandler(IMediator mediator) : IRequestHandler<UpdateStatusCommand, int?>
{
    public async Task<int?> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new UpdateStatusEntityCommand(request.Id, request.Name, request.IsFinished), cancellationToken);
    }
}
