using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetDelayedTaskEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetDelayedTasks;

public class GetDelayedTasksQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetDelayedTasksQuery, List<GetTaskDTO>>
{
    public async Task<List<GetTaskDTO>> Handle(GetDelayedTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await mediator.Send(new GetDelayedTaskEntitiesQuery(), cancellationToken);
        return mapper.Map<List<GetTaskDTO>>(tasks);
    }
}
