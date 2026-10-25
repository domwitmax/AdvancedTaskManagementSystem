using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteTypeEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteType;

public class DeleteTypeCommandHandler(IMediator mediator) : IRequestHandler<DeleteTypeCommand, bool>
{
    public async Task<bool> Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteTypeEntityCommand(request.Id), cancellationToken);
    }
}
