using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetAttachmentEntityById;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Tasks.GetAttachmentEntityById;

public class GetAttachmentEntityByIdQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetAttachmentEntityByIdQuery, TaskAttachmentEntityDTO?>
{
    public async Task<TaskAttachmentEntityDTO?> Handle(GetAttachmentEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var attachment = await dbContext.TaskAttachments
            .SingleOrDefaultAsync(item => item.Id == request.AttachmentId, cancellationToken);

        return mapper.Map<TaskAttachmentEntityDTO?>(attachment);
    }
}
