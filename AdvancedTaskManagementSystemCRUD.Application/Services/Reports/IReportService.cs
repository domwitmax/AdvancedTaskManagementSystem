using AdvancedTaskManagementSystemCRUD.Application.Services.Reports.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Reports;

public interface IReportService
{
    Task<List<TasksByStatusDTO>> GetTasksByStatus();
    Task<List<TaskDTO>> GetDelayedTasks();
    Task<List<TaskDTO>> GetUserTasks(string userName);
    Task<ProjectProgressDTO?> GetProjectProgress(int projectId);
    Task<List<UserWorkloadDTO>> GetUserWorkload();
}
