using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddPriorityEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddPriority;

public class AddPriorityCommandHandler(IMediator mediator) : IRequestHandler<AddPriorityCommand, int>
{
    public async Task<int> Handle(AddPriorityCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new AddPriorityEntityCommand(request.Name), cancellationToken);
    }
}
