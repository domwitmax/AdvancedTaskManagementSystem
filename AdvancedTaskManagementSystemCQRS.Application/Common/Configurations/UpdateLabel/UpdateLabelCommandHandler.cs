using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateLabelEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateLabel;

public class UpdateLabelCommandHandler(IMediator mediator) : IRequestHandler<UpdateLabelCommand, int?>
{
    public async Task<int?> Handle(UpdateLabelCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new UpdateLabelEntityCommand(request.Id, request.Name, request.Description), cancellationToken);
    }
}
