using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetListAllTasks
{
    public class GetListAllTasksQueryCommand(IMediator mediator, IMapper mapper) : IRequestHandler<GetListAllTasksQuery, List<GetTaskDTO>>
    {
        public async Task<List<GetTaskDTO>> Handle(GetListAllTasksQuery request, CancellationToken cancellationToken)
        {
            var task = await mediator.Send(new GetTaskEntitiesQuery(), cancellationToken);
            return mapper.Map<List<GetTaskDTO>>(task);
        }
    }
}
