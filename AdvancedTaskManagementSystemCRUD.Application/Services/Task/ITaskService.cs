using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Task
{
    public interface ITaskService
    {
        Task<int> CreateTask(TaskDTO taskDTO);
        Task<int> EditTask(TaskDTO taskDTO, string changedBy);
        Task<int> AssignmentTask(AssignmentTaskDTO assignmentTaskDTO, string changedBy);
        Task<int> ChangeTaskStatus(ChangeTaskStatusDTO changeTaskStatusDTO, string changedBy);
        Task<TaskDTO?> GetTaskById(int taskId);
        Task<List<TaskDTO>> GetTaskByAssignment(string assignment);
        Task<List<TaskDTO>> GetTaskByCreator(string creator);
        Task<List<TaskDTO>> GetFilterListTasks(FilterListTasksDTO filterListTasksDTO);
        Task<List<TaskDTO>> GetAllTasks();
        Task<bool> DeleteTask(int taskId);
        Task<int?> AddAttachment(TaskAttachmentDTO attachmentDTO);
        Task<List<TaskAttachmentDTO>> GetAttachmentsByTaskId(int taskId);
        Task<TaskAttachmentDTO?> GetAttachmentById(int attachmentId);
    }
}
