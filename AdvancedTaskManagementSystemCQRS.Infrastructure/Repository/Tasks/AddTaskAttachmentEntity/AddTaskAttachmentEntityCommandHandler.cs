using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.AddTaskAttachmentEntity;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Tasks.AddTaskAttachmentEntity;

public class AddTaskAttachmentEntityCommandHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<AddTaskAttachmentEntityCommand, int?>
{
    public async Task<int?> Handle(AddTaskAttachmentEntityCommand request, CancellationToken cancellationToken)
    {
        var attachment = mapper.Map<TaskAttachmentEntity>(request);
        dbContext.TaskAttachments.Add(attachment);

        if (request.ChangeDetails.Count > 0)
        {
            dbContext.ChangesHistory.Add(new ChangesHistoryEntity
            {
                TaskId = request.TaskId,
                ChangedBy = request.UploadedBy,
                ChangedAt = DateTime.UtcNow,
                ChangeDetails = MapChangeDetails(request.ChangeDetails)
            });
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return attachment.Id;
    }

    private static List<ChangeDetailEntity> MapChangeDetails(IEnumerable<TaskChangeDetailDTO> changeDetails)
        => changeDetails.Select(item => new ChangeDetailEntity
        {
            PropertyName = item.PropertyName,
            OldValue = item.OldValue,
            NewValue = item.NewValue
        }).ToList();
}
