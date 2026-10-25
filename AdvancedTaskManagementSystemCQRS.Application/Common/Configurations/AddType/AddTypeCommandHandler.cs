using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddTypeEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddType;

public class AddTypeCommandHandler(IMediator mediator) : IRequestHandler<AddTypeCommand, int>
{
    public async Task<int> Handle(AddTypeCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new AddTypeEntityCommand(request.Name), cancellationToken);
    }
}
