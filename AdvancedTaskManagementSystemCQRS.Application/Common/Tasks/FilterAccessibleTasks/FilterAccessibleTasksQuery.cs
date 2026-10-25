using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.FilterAccessibleTasks;

public record FilterAccessibleTasksQuery(
    List<GetTaskDTO> Tasks,
    string? UserLogin,
    bool IsAdministrator) : IRequest<List<GetTaskDTO>>;
