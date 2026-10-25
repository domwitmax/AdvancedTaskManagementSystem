using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetUserTasks;

public class GetUserTasksQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetUserTasksQuery, List<GetTaskDTO>>
{
    public async Task<List<GetTaskDTO>> Handle(GetUserTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await mediator.Send(new GetTaskEntitiesQuery(), cancellationToken);
        return mapper.Map<List<GetTaskDTO>>(tasks.Where(t => t.AssignedTo == request.UserName).ToList());
    }
}