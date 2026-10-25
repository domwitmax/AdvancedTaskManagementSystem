using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetTasksByStatus;

public class GetTasksByStatusQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetTasksByStatusQuery, List<TasksByStatusDTO>>
{
    public async Task<List<TasksByStatusDTO>> Handle(GetTasksByStatusQuery request, CancellationToken cancellationToken)
    {
        var tasks = await mediator.Send(new GetTaskEntitiesQuery(), cancellationToken);
        var groupedTasks = tasks.GroupBy(t => t.Status).Select(g => (Status: g.Key, Count: g.Count())).ToList();
        return mapper.Map<List<TasksByStatusDTO>>(groupedTasks);
    }
}
