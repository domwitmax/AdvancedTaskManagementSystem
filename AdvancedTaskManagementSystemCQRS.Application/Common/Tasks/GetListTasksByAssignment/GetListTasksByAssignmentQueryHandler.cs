using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetListTasksByAssignment
{
    public class GetListTasksByAssignmentQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetListTasksByAssignmentQuery, List<GetTaskDTO>>
    {
        public async Task<List<GetTaskDTO>> Handle(GetListTasksByAssignmentQuery request, CancellationToken cancellationToken)
        {
            var allTasksRaw = await mediator.Send(new GetTaskEntitiesQuery(), cancellationToken);
            var allTasks = mapper.Map<List<GetTaskDTO>>(allTasksRaw);
            return allTasks.Where(item => item.AssignedTo == request.Assignment).ToList();
        }
    }
}
