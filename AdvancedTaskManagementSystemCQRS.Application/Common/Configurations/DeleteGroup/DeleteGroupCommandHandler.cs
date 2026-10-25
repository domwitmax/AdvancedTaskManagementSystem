using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteGroupEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteGroup;

public class DeleteGroupCommandHandler(IMediator mediator) : IRequestHandler<DeleteGroupCommand, bool>
{
    public async Task<bool> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteGroupEntityCommand(request.Name), cancellationToken);
    }
}
