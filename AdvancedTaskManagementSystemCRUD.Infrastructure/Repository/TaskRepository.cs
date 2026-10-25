using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Repository.Tasks.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.Repository
{
    public class TaskRepository(TaskManagerDbContext dbContext, IMapper mapper) : ITaskRepository
    {
        public async Task<int> CreateTask(TaskEntityDTO taskEntityDTO)
        {
            var account = await dbContext.Account
                .FirstOrDefaultAsync(item => item.Login == taskEntityDTO.CreatedBy && item.Active);
            if (account == null)
                return 0;

            var requestedLabelIds = taskEntityDTO.LabelIds?.Distinct().ToList() ?? new List<int>();
            var labelIds = await GetExistingLabelIds(requestedLabelIds);
            if (requestedLabelIds.Count != labelIds.Count)
                return 0;

            var taskEntity = mapper.Map<TaskEntity>(taskEntityDTO);
            taskEntity.Creator = account;
            foreach (var labelId in labelIds)
            {
                taskEntity.TaskLabels.Add(new TaskLabelEntity { LabelId = labelId });
            }

            var history = new ChangesHistoryEntity
            {
                Task = taskEntity,
                ChangedBy = taskEntityDTO.CreatedBy,
                ChangedAt = DateTime.UtcNow,
                ChangeDetails = await GetCreateDetails(taskEntityDTO, labelIds)
            };

            dbContext.Task.Add(taskEntity);
            dbContext.ChangesHistory.Add(history);
            await dbContext.SaveChangesAsync();
            return taskEntity.Id;
        }

        public async Task<int> UpdateTask(TaskEntityDTO taskEntityDTO)
        {
            var task = await dbContext.Task
                .Include(item => item.Status)
                .Include(item => item.Type)
                .Include(item => item.Priority)
                .Include(item => item.Project)
                .Include(item => item.TaskLabels)
                    .ThenInclude(item => item.Label)
                .FirstOrDefaultAsync(x => x.Id == taskEntityDTO.Id);

            if (task == null)
                return 0;

            var requestedLabelIds = taskEntityDTO.LabelIds?.Distinct().ToList() ?? new List<int>();
            var labelIds = await GetExistingLabelIds(requestedLabelIds);
            if (requestedLabelIds.Count != labelIds.Count)
                return 0;

            var changeDetails = await GetChangeDetails(task, taskEntityDTO, labelIds);

            var createdAt = task.CreatedAt;
            var createdBy = task.CreatedBy;
            mapper.Map(taskEntityDTO, task);
            task.CreatedAt = createdAt;
            task.CreatedBy = createdBy;
            task.UpdatedAt = taskEntityDTO.UpdatedAt ?? DateTime.UtcNow;

            await ApplyTaskLabels(task.Id, labelIds);
            await AddChangesHistory(task.Id, taskEntityDTO.ChangedBy, changeDetails);
            await dbContext.SaveChangesAsync();

            return task.Id;
        }

        public async Task<TaskEntityDTO?> GetTaskById(int id)
        {
            var taskEntity = await dbContext.Task
                .Include(item => item.Creator)
                .Include(item => item.Status)
                .Include(item => item.Type)
                .Include(item => item.Priority)
                .Include(item => item.Project)
                .Include(item => item.TaskLabels)
                    .ThenInclude(item => item.Label)
                .SingleOrDefaultAsync(item => item.Id == id);
            return mapper.Map<TaskEntityDTO?>(taskEntity);
        }

        public async Task<List<TaskEntityDTO>> GetTaskEntities()
        {
            var taskEntities = await dbContext.Task
                .Include(item => item.Status)
                .Include(item => item.Type)
                .Include(item => item.Priority)
                .Include(item => item.Project)
                .Include(item => item.TaskLabels)
                    .ThenInclude(item => item.Label)
                .ToListAsync();
            return mapper.Map<List<TaskEntityDTO>>(taskEntities);
        }

        public async Task<bool> DeleteTask(int id)
        {
            var taskEntity = dbContext.Task.Find(id);
            if (taskEntity == null)
            {
                return false;
            }
            dbContext.Task.Remove(taskEntity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<int?> AddAttachment(TaskAttachmentEntityDTO attachmentEntityDTO)
        {
            var taskExists = await dbContext.Task.AnyAsync(item => item.Id == attachmentEntityDTO.TaskId);
            if (!taskExists)
                return null;

            var uploaderExists = await dbContext.Account
                .AsNoTracking()
                .AnyAsync(item => item.Login == attachmentEntityDTO.UploadedBy && item.Active);
            if (!uploaderExists)
                return null;

            var attachment = mapper.Map<TaskAttachmentEntity>(attachmentEntityDTO);
            dbContext.TaskAttachments.Add(attachment);
            await AddAttachmentHistory(attachmentEntityDTO.TaskId, attachmentEntityDTO.UploadedBy, attachmentEntityDTO.FileName);
            await dbContext.SaveChangesAsync();
            return attachment.Id;
        }

        public async Task<List<TaskAttachmentEntityDTO>> GetAttachmentsByTaskId(int taskId)
        {
            var attachments = await dbContext.TaskAttachments
                .Where(item => item.TaskId == taskId)
                .OrderByDescending(item => item.UploadedAt)
                .ToListAsync();

            var attachmentDTOs = mapper.Map<List<TaskAttachmentEntityDTO>>(attachments);
            attachmentDTOs.ForEach(item => item.Content = null);
            return attachmentDTOs;
        }

        public async Task<TaskAttachmentEntityDTO?> GetAttachmentById(int attachmentId)
        {
            var attachment = await dbContext.TaskAttachments
                .SingleOrDefaultAsync(item => item.Id == attachmentId);

            return mapper.Map<TaskAttachmentEntityDTO?>(attachment);
        }


        private async Task<List<ChangeDetailEntity>> GetChangeDetails(TaskEntity task, TaskEntityDTO taskEntityDTO, List<int> labelIds)
        {
            var changeDetails = new List<ChangeDetailEntity>();

            AddChangeDetail(changeDetails, "Title", task.Title, taskEntityDTO.Title);
            AddChangeDetail(changeDetails, "Description", task.Description, taskEntityDTO.Description);
            AddChangeDetail(changeDetails, "DueDate", FormatDate(task.DueDate), FormatDate(taskEntityDTO.DueDate));
            AddChangeDetail(changeDetails, "AssignedTo", task.AssignedTo, taskEntityDTO.AssignedTo, true);

            if (task.TypeId != taskEntityDTO.TypeId)
            {
                var newType = await dbContext.Type
                    .AsNoTracking()
                    .SingleOrDefaultAsync(item => item.Id == taskEntityDTO.TypeId);
                AddChangeDetail(changeDetails, "Type", task.Type?.Name ?? task.TypeId.ToString(), newType?.Name ?? taskEntityDTO.TypeId.ToString());
            }

            if (task.StatusId != taskEntityDTO.StatusId)
            {
                var newStatus = await dbContext.Status
                    .AsNoTracking()
                    .SingleOrDefaultAsync(item => item.Id == taskEntityDTO.StatusId);
                AddChangeDetail(changeDetails, "Status", task.Status?.Name ?? task.StatusId.ToString(), newStatus?.Name ?? taskEntityDTO.StatusId.ToString());
            }

            if (task.ProjectId != taskEntityDTO.ProjectId)
            {
                var newProject = await dbContext.Projects
                    .AsNoTracking()
                    .SingleOrDefaultAsync(item => item.Id == taskEntityDTO.ProjectId);
                AddChangeDetail(changeDetails, "Project", task.Project?.Name ?? task.ProjectId.ToString(), newProject?.Name ?? taskEntityDTO.ProjectId.ToString());
            }

            if (task.PriorityId != taskEntityDTO.PriorityId)
            {
                var newPriority = await dbContext.Priorities
                    .AsNoTracking()
                    .SingleOrDefaultAsync(item => item.Id == taskEntityDTO.PriorityId);
                AddChangeDetail(changeDetails, "Priority", task.Priority?.Name ?? task.PriorityId.ToString(), newPriority?.Name ?? taskEntityDTO.PriorityId.ToString());
            }

            var oldLabelIds = task.TaskLabels
                .Select(item => item.LabelId)
                .Distinct()
                .OrderBy(item => item)
                .ToList();

            var newLabelIds = labelIds
                .Distinct()
                .OrderBy(item => item)
                .ToList();

            if (!oldLabelIds.SequenceEqual(newLabelIds))
            {
                var oldLabels = task.TaskLabels
                    .Where(item => item.Label != null)
                    .Select(item => item.Label.Name)
                    .OrderBy(item => item)
                    .ToList();

                var newLabels = await dbContext.Labels
                    .AsNoTracking()
                    .Where(item => newLabelIds.Contains(item.Id))
                    .OrderBy(item => item.Name)
                    .Select(item => item.Name)
                    .ToListAsync();

                AddChangeDetail(changeDetails, "Labels", string.Join(", ", oldLabels), string.Join(", ", newLabels));
            }

            return changeDetails;
        }

        private async Task<List<int>> GetExistingLabelIds(List<int> labelIds)
        {
            var distinctLabelIds = labelIds?.Distinct().ToList() ?? new List<int>();
            if (distinctLabelIds.Count == 0)
            {
                return new List<int>();
            }

            return await dbContext.Labels
                .AsNoTracking()
                .Where(item => distinctLabelIds.Contains(item.Id))
                .Select(item => item.Id)
                .ToListAsync();
        }

        private void AddChangeDetail(List<ChangeDetailEntity> changeDetails, string propertyName, string? oldValue, string? newValue, bool ignoreCase = false)
        {
            var oldValueToCompare = oldValue ?? string.Empty;
            var newValueToCompare = newValue ?? string.Empty;
            var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            if (string.Equals(oldValueToCompare, newValueToCompare, comparison))
            {
                return;
            }

            changeDetails.Add(new ChangeDetailEntity
            {
                PropertyName = FormatValue(propertyName),
                OldValue = FormatValue(oldValueToCompare),
                NewValue = FormatValue(newValueToCompare)
            });
        }

        private async Task AddChangesHistory(int taskId, string changedBy, List<ChangeDetailEntity> changeDetails)
        {
            if (changeDetails.Count == 0 || string.IsNullOrWhiteSpace(changedBy))
            {
                return;
            }

            var accountExists = await dbContext.Account
                .AsNoTracking()
                .AnyAsync(item => item.Login == changedBy);

            if (!accountExists)
            {
                return;
            }

            dbContext.ChangesHistory.Add(new ChangesHistoryEntity
            {
                TaskId = taskId,
                ChangedBy = changedBy,
                ChangedAt = DateTime.UtcNow,
                ChangeDetails = changeDetails
            });
        }

        private async Task AddCreateHistory(int taskId, TaskEntityDTO taskEntityDTO, List<int> labelIds)
        {
            if (string.IsNullOrWhiteSpace(taskEntityDTO.CreatedBy))
            {
                return;
            }

            var accountExists = await dbContext.Account
                .AsNoTracking()
                .AnyAsync(item => item.Login == taskEntityDTO.CreatedBy);

            if (!accountExists)
            {
                return;
            }

            var changeDetails = await GetCreateDetails(taskEntityDTO, labelIds);
            dbContext.ChangesHistory.Add(new ChangesHistoryEntity
            {
                TaskId = taskId,
                ChangedBy = taskEntityDTO.CreatedBy,
                ChangedAt = DateTime.UtcNow,
                ChangeDetails = changeDetails
            });
        }

        private async Task<List<ChangeDetailEntity>> GetCreateDetails(TaskEntityDTO taskEntityDTO, List<int> labelIds)
        {
            var type = await dbContext.Type.AsNoTracking().SingleOrDefaultAsync(item => item.Id == taskEntityDTO.TypeId);
            var status = await dbContext.Status.AsNoTracking().SingleOrDefaultAsync(item => item.Id == taskEntityDTO.StatusId);
            var project = await dbContext.Projects.AsNoTracking().SingleOrDefaultAsync(item => item.Id == taskEntityDTO.ProjectId);
            var priority = await dbContext.Priorities.AsNoTracking().SingleOrDefaultAsync(item => item.Id == taskEntityDTO.PriorityId);
            var labels = await dbContext.Labels
                .AsNoTracking()
                .Where(item => labelIds.Contains(item.Id))
                .OrderBy(item => item.Name)
                .Select(item => item.Name)
                .ToListAsync();

            var changeDetails = new List<ChangeDetailEntity>
            {
                CreateChangeDetail("Task", string.Empty, "Utworzono zadanie"),
                CreateChangeDetail("Title", string.Empty, taskEntityDTO.Title),
                CreateChangeDetail("Description", string.Empty, taskEntityDTO.Description),
                CreateChangeDetail("Type", string.Empty, type?.Name ?? taskEntityDTO.TypeId.ToString()),
                CreateChangeDetail("Status", string.Empty, status?.Name ?? taskEntityDTO.StatusId.ToString()),
                CreateChangeDetail("Project", string.Empty, project?.Name ?? taskEntityDTO.ProjectId.ToString()),
                CreateChangeDetail("Priority", string.Empty, priority?.Name ?? taskEntityDTO.PriorityId.ToString()),
                CreateChangeDetail("DueDate", string.Empty, FormatDate(taskEntityDTO.DueDate)),
                CreateChangeDetail("AssignedTo", string.Empty, taskEntityDTO.AssignedTo),
                CreateChangeDetail("Labels", string.Empty, string.Join(", ", labels))
            };

            return changeDetails.Where(item => !string.IsNullOrEmpty(item.NewValue)).ToList();
        }

        private async Task AddAttachmentHistory(int taskId, string changedBy, string fileName)
        {
            if (string.IsNullOrWhiteSpace(changedBy))
            {
                return;
            }

            var accountExists = await dbContext.Account
                .AsNoTracking()
                .AnyAsync(item => item.Login == changedBy);

            if (!accountExists)
            {
                return;
            }

            dbContext.ChangesHistory.Add(new ChangesHistoryEntity
            {
                TaskId = taskId,
                ChangedBy = changedBy,
                ChangedAt = DateTime.UtcNow,
                ChangeDetails = new List<ChangeDetailEntity>
                {
                    new()
                    {
                        PropertyName = "Attachment",
                        OldValue = string.Empty,
                        NewValue = FormatValue(fileName)
                    }
                }
            });
        }

        private async Task ApplyTaskLabels(int taskId, List<int> labelIds)
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

        private static ChangeDetailEntity CreateChangeDetail(string propertyName, string? oldValue, string? newValue)
        {
            return new ChangeDetailEntity
            {
                PropertyName = FormatValue(propertyName),
                OldValue = FormatValue(oldValue),
                NewValue = FormatValue(newValue)
            };
        }

        private static string FormatDate(DateTime? value)
        {
            return value?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty;
        }

        private static string FormatValue(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return value.Length <= 100 ? value : value[..97] + "...";
        }
    }
}
