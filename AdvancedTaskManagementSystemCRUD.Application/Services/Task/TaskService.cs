using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Repository.Tasks.DTOs;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Task
{
    public class TaskService(ITaskRepository taskRepository, IMapper mapper) : ITaskService
    {
        public async Task<int> CreateTask(TaskDTO taskDTO)
        {
            var taskEntity = mapper.Map<TaskEntityDTO>(taskDTO);
            taskEntity.CreatedAt = DateTime.UtcNow;
            return await taskRepository.CreateTask(taskEntity);
        }

        public async Task<int> EditTask(TaskDTO taskDTO, string changedBy)
        {
            var taskEntityDTO = mapper.Map<TaskEntityDTO>(taskDTO);
            taskEntityDTO.UpdatedAt = DateTime.UtcNow;
            taskEntityDTO.ChangedBy = changedBy;
            return await taskRepository.UpdateTask(taskEntityDTO);
        }

        public async Task<int> AssignmentTask(AssignmentTaskDTO assignmentTaskDTO, string changedBy)
        {
            var taskEntity = await taskRepository.GetTaskById(assignmentTaskDTO.Id);
            if (taskEntity == null)
            {
                return 0;
            }
            taskEntity.AssignedTo = assignmentTaskDTO.AssignedTo;
            taskEntity.ChangedBy = changedBy;
            return await taskRepository.UpdateTask(taskEntity);
        }

        public async Task<int> ChangeTaskStatus(ChangeTaskStatusDTO changeTaskStatusDTO, string changedBy)
        {
            var taskEntity = await taskRepository.GetTaskById(changeTaskStatusDTO.Id);
            if (taskEntity == null)
            {
                return 0;
            }
            taskEntity.StatusId = changeTaskStatusDTO.StatusId;
            taskEntity.ChangedBy = changedBy;
            return await taskRepository.UpdateTask(taskEntity);
        }

        public async Task<TaskDTO?> GetTaskById(int taskId)
        {
            var taskEntity = await taskRepository.GetTaskById(taskId);
            return mapper.Map<TaskDTO>(taskEntity);
        }

        public async Task<List<TaskDTO>> GetTaskByAssignment(string assignment)
        {
            var allTasksRaw = await taskRepository.GetTaskEntities();
            var allTasks = mapper.Map<List<TaskDTO>>(allTasksRaw);
            return allTasks.Where(item => item.AssignedTo == assignment).ToList();
        }

        public async Task<List<TaskDTO>> GetTaskByCreator(string creator)
        {
            var allTasksRaw = await taskRepository.GetTaskEntities();
            var allTasks = mapper.Map<List<TaskDTO>>(allTasksRaw);
            return allTasks.Where(item => item.CreatedBy == creator).ToList();
        }

        public async Task<List<TaskDTO>> GetFilterListTasks(FilterListTasksDTO filterListTasksDTO)
        {
            var filteredTasks = await taskRepository.GetTaskEntities();

            if (filterListTasksDTO.ByTypeId != null)
            {
                filteredTasks = filteredTasks.Where(t => t.TypeId == filterListTasksDTO.ByTypeId).ToList();
            }

            if (filterListTasksDTO.ByStatusId != null)
            {
                filteredTasks = filteredTasks.Where(t => t.StatusId == filterListTasksDTO.ByStatusId).ToList();
            }

            if (!string.IsNullOrEmpty(filterListTasksDTO.ByPriority))
            {
                filteredTasks = filteredTasks.Where(t => t.Priority.Equals(filterListTasksDTO.ByPriority, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(filterListTasksDTO.ByCreator))
            {
                filteredTasks = filteredTasks.Where(t => t.CreatedBy.Equals(filterListTasksDTO.ByCreator, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(filterListTasksDTO.ByAssignedTo))
            {
                filteredTasks = filteredTasks.Where(t => t.AssignedTo.Equals(filterListTasksDTO.ByAssignedTo, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(filterListTasksDTO.ByLabel))
            {
                filteredTasks = filteredTasks.Where(t => t.Labels.Any(label => label.Equals(filterListTasksDTO.ByLabel, StringComparison.OrdinalIgnoreCase))).ToList();
            }

            if (filterListTasksDTO.BetweenCreatedBegin != null)
            {
                filteredTasks = filteredTasks.Where(t => t.CreatedAt >= filterListTasksDTO.BetweenCreatedBegin).ToList();
            }

            if (filterListTasksDTO.BetweenCreatedEnd != null)
            {
                filteredTasks = filteredTasks.Where(t => t.CreatedAt <= filterListTasksDTO.BetweenCreatedEnd).ToList();
            }

            if (filterListTasksDTO.BetweenUpdatedBegin != null)
            {
                filteredTasks = filteredTasks.Where(t => t.UpdatedAt.HasValue && t.UpdatedAt.Value >= filterListTasksDTO.BetweenUpdatedBegin).ToList();
            }
            if (filterListTasksDTO.BetweenUpdatedEnd != null)
            {
                filteredTasks = filteredTasks.Where(t => t.UpdatedAt.HasValue && t.UpdatedAt.Value <= filterListTasksDTO.BetweenUpdatedEnd).ToList();
            }

            if (filterListTasksDTO.BetweenDueDateBegin != null)
            {
                filteredTasks = filteredTasks.Where(t => t.DueDate.HasValue && t.DueDate.Value >= filterListTasksDTO.BetweenDueDateBegin).ToList();
            }

            if (filterListTasksDTO.BetweenDueDateEnd != null)
            {
                filteredTasks = filteredTasks.Where(t => t.DueDate.HasValue && t.DueDate.Value <= filterListTasksDTO.BetweenDueDateEnd).ToList();
            }

            filteredTasks = SortTasks(filteredTasks, filterListTasksDTO.SortBy, filterListTasksDTO.SortDescending);

            return mapper.Map<List<TaskDTO>>(filteredTasks);
        }

        private static List<TaskEntityDTO> SortTasks(List<TaskEntityDTO> tasks, string? sortBy, bool sortDescending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                return tasks;
            }

            return sortBy.Trim().ToLowerInvariant() switch
            {
                "title" => sortDescending ? tasks.OrderByDescending(item => item.Title).ToList() : tasks.OrderBy(item => item.Title).ToList(),
                "type" => sortDescending ? tasks.OrderByDescending(item => item.Type).ToList() : tasks.OrderBy(item => item.Type).ToList(),
                "status" => sortDescending ? tasks.OrderByDescending(item => item.Status).ToList() : tasks.OrderBy(item => item.Status).ToList(),
                "project" => sortDescending ? tasks.OrderByDescending(item => item.ProjectName).ToList() : tasks.OrderBy(item => item.ProjectName).ToList(),
                "priority" => sortDescending ? tasks.OrderByDescending(item => item.Priority).ToList() : tasks.OrderBy(item => item.Priority).ToList(),
                "duedate" => sortDescending ? tasks.OrderByDescending(item => item.DueDate).ToList() : tasks.OrderBy(item => item.DueDate).ToList(),
                "createdat" => sortDescending ? tasks.OrderByDescending(item => item.CreatedAt).ToList() : tasks.OrderBy(item => item.CreatedAt).ToList(),
                "updatedat" => sortDescending ? tasks.OrderByDescending(item => item.UpdatedAt).ToList() : tasks.OrderBy(item => item.UpdatedAt).ToList(),
                "assignedto" => sortDescending ? tasks.OrderByDescending(item => item.AssignedTo).ToList() : tasks.OrderBy(item => item.AssignedTo).ToList(),
                "createdby" => sortDescending ? tasks.OrderByDescending(item => item.CreatedBy).ToList() : tasks.OrderBy(item => item.CreatedBy).ToList(),
                _ => tasks
            };
        }

        public async Task<List<TaskDTO>> GetAllTasks()
        {
            var task = await taskRepository.GetTaskEntities();
            return mapper.Map<List<TaskDTO>>(task);
        }


        public async Task<int?> AddAttachment(TaskAttachmentDTO attachmentDTO)
        {
            var attachmentEntity = mapper.Map<TaskAttachmentEntityDTO>(attachmentDTO);
            attachmentEntity.UploadedAt = DateTime.UtcNow;
            return await taskRepository.AddAttachment(attachmentEntity);
        }

        public async Task<List<TaskAttachmentDTO>> GetAttachmentsByTaskId(int taskId)
        {
            var attachments = await taskRepository.GetAttachmentsByTaskId(taskId);
            return mapper.Map<List<TaskAttachmentDTO>>(attachments);
        }

        public async Task<TaskAttachmentDTO?> GetAttachmentById(int attachmentId)
        {
            var attachment = await taskRepository.GetAttachmentById(attachmentId);
            return mapper.Map<TaskAttachmentDTO?>(attachment);
        }
        public async Task<bool> DeleteTask(int taskId)
        {
            return await taskRepository.DeleteTask(taskId);
        }
    }
}
