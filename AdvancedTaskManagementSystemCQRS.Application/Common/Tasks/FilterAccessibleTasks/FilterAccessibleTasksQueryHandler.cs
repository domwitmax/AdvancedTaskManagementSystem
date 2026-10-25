using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectsByUser;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.FilterAccessibleTasks;

public class FilterAccessibleTasksQueryHandler(IMediator mediator)
    : IRequestHandler<FilterAccessibleTasksQuery, List<GetTaskDTO>>
{
    public async Task<List<GetTaskDTO>> Handle(
        FilterAccessibleTasksQuery request,
        CancellationToken cancellationToken)
    {
        if (request.IsAdministrator)
            return request.Tasks;

        if (string.IsNullOrWhiteSpace(request.UserLogin))
            return new List<GetTaskDTO>();

        var projects = await mediator.Send(
            new GetProjectsByUserQuery(request.UserLogin),
            cancellationToken);

        var projectIds = projects.Select(project => project.Id).ToHashSet();
        return request.Tasks
            .Where(task => projectIds.Contains(task.ProjectId))
            .ToList();
    }
}
