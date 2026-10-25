using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteLabelEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteLabel;

public class DeleteLabelCommandHandler(IMediator mediator) : IRequestHandler<DeleteLabelCommand, bool>
{
    public async Task<bool> Handle(DeleteLabelCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteLabelEntityCommand(request.Id), cancellationToken);
    }
}
