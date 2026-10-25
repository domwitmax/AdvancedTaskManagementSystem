using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateTypeEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateType;

public class UpdateTypeCommandHandler(IMediator mediator) : IRequestHandler<UpdateTypeCommand, int?>
{
    public async Task<int?> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new UpdateTypeEntityCommand(request.Id, request.Name), cancellationToken);
    }
}
