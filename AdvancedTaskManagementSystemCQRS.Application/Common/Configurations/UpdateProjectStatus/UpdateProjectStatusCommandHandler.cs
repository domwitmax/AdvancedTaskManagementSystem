using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateProjectStatusEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateProjectStatus;

public class UpdateProjectStatusCommandHandler(IMediator mediator) : IRequestHandler<UpdateProjectStatusCommand, int?>
{
    public async Task<int?> Handle(UpdateProjectStatusCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new UpdateProjectStatusEntityCommand(request.Id, request.Name), cancellationToken);
    }
}
