using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetAttachmentEntityById;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetAttachmentById;

public class GetAttachmentByIdQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetAttachmentByIdQuery, TaskAttachmentDTO?>
{
    public async Task<TaskAttachmentDTO?> Handle(GetAttachmentByIdQuery request, CancellationToken cancellationToken)
    {
        var attachment = await mediator.Send(new GetAttachmentEntityByIdQuery(request.AttachmentId), cancellationToken);
        return mapper.Map<TaskAttachmentDTO?>(attachment);
    }
}
