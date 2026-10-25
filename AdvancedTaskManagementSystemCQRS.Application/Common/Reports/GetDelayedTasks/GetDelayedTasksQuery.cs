using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetDelayedTasks;

public record GetDelayedTasksQuery() : IRequest<List<GetTaskDTO>>;
