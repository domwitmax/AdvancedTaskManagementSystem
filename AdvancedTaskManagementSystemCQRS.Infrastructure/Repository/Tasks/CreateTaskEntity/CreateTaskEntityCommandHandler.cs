using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.CreateTaskEntity;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Tasks.CreateTaskEntity;

public class CreateTasksEntityCommandHandler(TaskManagerDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateTaskEntityCommand, int>
{
    public async Task<int> Handle(CreateTaskEntityCommand request, CancellationToken cancellationToken)
    {
        var taskEntity = mapper.Map<TaskEntity>(request);

        foreach (var labelId in request.LabelIds.Distinct())
            taskEntity.TaskLabels.Add(new TaskLabelEntity { LabelId = labelId });

        dbContext.Task.Add(taskEntity);

        if (request.ChangeDetails.Count > 0)
        {
            dbContext.ChangesHistory.Add(new ChangesHistoryEntity
            {
                Task = taskEntity,
                ChangedBy = request.CreatedBy,
                ChangedAt = DateTime.UtcNow,
                ChangeDetails = MapChangeDetails(request.ChangeDetails)
            });
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return taskEntity.Id;
    }

    private static List<ChangeDetailEntity> MapChangeDetails(IEnumerable<TaskChangeDetailDTO> changeDetails)
        => changeDetails.Select(item => new ChangeDetailEntity
        {
            PropertyName = item.PropertyName,
            OldValue = item.OldValue,
            NewValue = item.NewValue
        }).ToList();
}
