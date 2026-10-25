using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteProjectStatusEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteProjectStatus;

public class DeleteProjectStatusCommandHandler(IMediator mediator) : IRequestHandler<DeleteProjectStatusCommand, bool>
{
    public async Task<bool> Handle(DeleteProjectStatusCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteProjectStatusEntityCommand(request.Id), cancellationToken);
    }
}
