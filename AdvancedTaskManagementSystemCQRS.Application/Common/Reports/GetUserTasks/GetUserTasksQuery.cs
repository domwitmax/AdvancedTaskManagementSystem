using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetUserTasks;

public record GetUserTasksQuery(string UserName) : IRequest<List<GetTaskDTO>>;
