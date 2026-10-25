using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddLabelEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddLabel;

public class AddLabelCommandHandler(IMediator mediator) : IRequestHandler<AddLabelCommand, int>
{
    public async Task<int> Handle(AddLabelCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new AddLabelEntityCommand(request.Name, request.Description), cancellationToken);
    }
}
