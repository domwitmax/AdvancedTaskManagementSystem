using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddStatusEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddStatus;

public class AddStatusCommandHandler(IMediator mediator) : IRequestHandler<AddStatusCommand, int>
{
    public async Task<int> Handle(AddStatusCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new AddStatusEntityCommand(request.Name, request.TypeId, request.IsFinished), cancellationToken);
    }
}
