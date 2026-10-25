using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.UpdateTaskEntity;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Tasks.UpdateTaskEntity
{
    public class UpdateTaskEntityCommandHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateTaskEntityCommand, int>
    {
        public async Task<int> Handle(UpdateTaskEntityCommand request, CancellationToken cancellationToken)
        {
            var taskEntity = await dbContext.Task
                .SingleOrDefaultAsync(item => item.Id == request.Id, cancellationToken);
            if (taskEntity == null)
                return 0;

            mapper.Map(request, taskEntity);

            dbContext.Task.Update(taskEntity);
            ApplyTaskLabels(taskEntity.Id, request.LabelIds);
            AddChangesHistory(taskEntity.Id, request.ChangedBy, request.ChangeDetails);
            await dbContext.SaveChangesAsync(cancellationToken);

            return taskEntity.Id;
        }

        private void AddChangesHistory(
            int taskId,
            string changedBy,
            IReadOnlyCollection<TaskChangeDetailDTO> changeDetails)
        {
            if (changeDetails.Count == 0)
                return;

            dbContext.ChangesHistory.Add(new ChangesHistoryEntity
            {
                TaskId = taskId,
                ChangedBy = changedBy,
                ChangedAt = DateTime.UtcNow,
                ChangeDetails = MapChangeDetails(changeDetails)
            });
        }

        private void ApplyTaskLabels(int taskId, IEnumerable<int> labelIds)
        {
            var currentLabels = dbContext.TaskLabels.Where(item => item.TaskId == taskId);
            dbContext.TaskLabels.RemoveRange(currentLabels);

            foreach (var labelId in labelIds.Distinct())
            {
                dbContext.TaskLabels.Add(new TaskLabelEntity
                {
                    TaskId = taskId,
                    LabelId = labelId
                });
            }
        }

        private static List<ChangeDetailEntity> MapChangeDetails(IEnumerable<TaskChangeDetailDTO> changeDetails)
            => changeDetails.Select(item => new ChangeDetailEntity
            {
                PropertyName = item.PropertyName,
                OldValue = item.OldValue,
                NewValue = item.NewValue
            }).ToList();
    }
}
