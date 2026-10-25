using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetListTasksByCreator
{
    public class GetListTasksByCreatorQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetListTasksByCreatorQuery, List<GetTaskDTO>>
    {
        public async Task<List<GetTaskDTO>> Handle(GetListTasksByCreatorQuery request, CancellationToken cancellationToken)
        {
            var allTasksRaw = await mediator.Send(new GetTaskEntitiesQuery(), cancellationToken);
            var allTasks = mapper.Map<List<GetTaskDTO>>(allTasksRaw);
            return allTasks.Where(item => item.CreatedBy == request.Creator).ToList();
        }
    }
}
