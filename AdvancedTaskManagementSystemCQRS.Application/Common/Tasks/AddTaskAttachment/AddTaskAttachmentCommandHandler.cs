using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.History;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.AddTaskAttachmentEntity;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.AddTaskAttachment;

public class AddTaskAttachmentCommandHandler(IMediator mediator, IMapper mapper) : IRequestHandler<AddTaskAttachmentCommand, int?>
{
    public async Task<int?> Handle(AddTaskAttachmentCommand request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<AddTaskAttachmentEntityCommand>(request);
        command.ChangeDetails = TaskChangeDetailsBuilder.ForAttachment(request.FileName);
        return await mediator.Send(command, cancellationToken);
    }
}
