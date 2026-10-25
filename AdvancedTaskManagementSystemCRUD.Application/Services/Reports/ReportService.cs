using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Services.Reports.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Reports;

public class ReportService(
    ITaskRepository taskRepository,
    IAccountRepository accountRepository,
    IProjectRepository projectRepository,
    IMapper mapper) : IReportService
{
    public async Task<List<TasksByStatusDTO>> GetTasksByStatus()
    {
        var tasks = await taskRepository.GetTaskEntities();
        var groupedTasks = tasks
            .GroupBy(item => item.Status)
            .Select(item => (Status: item.Key, Count: item.Count()))
            .ToList();
        return mapper.Map<List<TasksByStatusDTO>>(groupedTasks);
    }

    public async Task<List<TaskDTO>> GetDelayedTasks()
    {
        var tasks = await taskRepository.GetTaskEntities();
        var delayedTasks = tasks
            .Where(item => item.DueDate.HasValue && item.DueDate.Value < DateTime.UtcNow && !item.StatusIsFinished)
            .ToList();
        return mapper.Map<List<TaskDTO>>(delayedTasks);
    }

    public async Task<List<TaskDTO>> GetUserTasks(string userName)
    {
        var tasks = await taskRepository.GetTaskEntities();
        return mapper.Map<List<TaskDTO>>(tasks
            .Where(item => item.AssignedTo.Equals(userName, StringComparison.OrdinalIgnoreCase))
            .ToList());
    }

    public async Task<ProjectProgressDTO?> GetProjectProgress(int projectId)
    {
        if (projectRepository.GetProjectByIdAsync(projectId) == null)
            return null;

        var tasks = await taskRepository.GetTaskEntities();
        var projectTasks = tasks.Where(item => item.ProjectId == projectId).ToList();
        return mapper.Map<ProjectProgressDTO>((
            ProjectId: projectId,
            AllTasks: projectTasks.Count,
            CompletedTasks: projectTasks.Count(item => item.StatusIsFinished)));
    }

    public async Task<List<UserWorkloadDTO>> GetUserWorkload()
    {
        var tasks = await taskRepository.GetTaskEntities();
        var accounts = await accountRepository.GetAccounts();
        var workloads = accounts
            .Where(account => account.Active)
            .OrderBy(account => account.Login)
            .Select(account => (
                AssignedTo: account.Login,
                TasksCount: tasks.Count(task => task.AssignedTo.Equals(account.Login, StringComparison.OrdinalIgnoreCase))))
            .ToList();
        return mapper.Map<List<UserWorkloadDTO>>(workloads);
    }
}
