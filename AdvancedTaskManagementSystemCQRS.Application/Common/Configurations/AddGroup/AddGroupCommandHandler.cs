using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddGroupEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddGroup;

public class AddGroupCommandHandler(IMediator mediator) : IRequestHandler<AddGroupCommand, int>
{
    public async Task<int> Handle(AddGroupCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new AddGroupEntityCommand(request.Name, request.Description), cancellationToken);
    }
}
