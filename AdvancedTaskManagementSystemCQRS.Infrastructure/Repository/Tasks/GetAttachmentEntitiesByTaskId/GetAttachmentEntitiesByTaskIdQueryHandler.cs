using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetAttachmentEntitiesByTaskId;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Tasks.GetAttachmentEntitiesByTaskId;

public class GetAttachmentEntitiesByTaskIdQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetAttachmentEntitiesByTaskIdQuery, List<TaskAttachmentEntityDTO>>
{
    public async Task<List<TaskAttachmentEntityDTO>> Handle(GetAttachmentEntitiesByTaskIdQuery request, CancellationToken cancellationToken)
    {
        var attachments = await dbContext.TaskAttachments
            .Where(item => item.TaskId == request.TaskId)
            .OrderByDescending(item => item.UploadedAt)
            .ToListAsync(cancellationToken);

        var attachmentDTOs = mapper.Map<List<TaskAttachmentEntityDTO>>(attachments);
        attachmentDTOs.ForEach(item => item.Content = null);
        return attachmentDTOs;
    }
}
