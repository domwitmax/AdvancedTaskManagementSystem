using AdvancedTaskManagementSystemCRUD.Application.Repository.Tasks.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Repository
{
    public interface ITaskRepository
    {
        Task<int> CreateTask(TaskEntityDTO taskEntityDTO);
        Task<int> UpdateTask(TaskEntityDTO taskEntityDTO);
        Task<TaskEntityDTO?> GetTaskById(int id);
        Task<List<TaskEntityDTO>> GetTaskEntities();
        Task<bool> DeleteTask(int id);
        Task<int?> AddAttachment(TaskAttachmentEntityDTO attachmentEntityDTO);
        Task<List<TaskAttachmentEntityDTO>> GetAttachmentsByTaskId(int taskId);
        Task<TaskAttachmentEntityDTO?> GetAttachmentById(int attachmentId);
    }
}
