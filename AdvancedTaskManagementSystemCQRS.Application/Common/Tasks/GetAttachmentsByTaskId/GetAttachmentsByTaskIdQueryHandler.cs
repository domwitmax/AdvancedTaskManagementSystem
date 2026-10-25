using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetAttachmentEntitiesByTaskId;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetAttachmentsByTaskId;

public class GetAttachmentsByTaskIdQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetAttachmentsByTaskIdQuery, List<TaskAttachmentDTO>>
{
    public async Task<List<TaskAttachmentDTO>> Handle(GetAttachmentsByTaskIdQuery request, CancellationToken cancellationToken)
    {
        var attachments = await mediator.Send(new GetAttachmentEntitiesByTaskIdQuery(request.TaskId), cancellationToken);
        return mapper.Map<List<TaskAttachmentDTO>>(attachments);
    }
}
