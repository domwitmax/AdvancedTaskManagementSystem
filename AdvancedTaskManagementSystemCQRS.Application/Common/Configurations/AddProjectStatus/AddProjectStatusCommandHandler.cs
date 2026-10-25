using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddProjectStatusEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddProjectStatus;

public class AddProjectStatusCommandHandler(IMediator mediator) : IRequestHandler<AddProjectStatusCommand, int>
{
    public async Task<int> Handle(AddProjectStatusCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new AddProjectStatusEntityCommand(request.Name), cancellationToken);
    }
}
