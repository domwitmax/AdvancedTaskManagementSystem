using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntityById;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetTaskById
{
    public class GetTaskByIdQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetTaskByIdQuery, GetTaskDTO?>
    {
        public async Task<GetTaskDTO?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var taskEntity = await mediator.Send(new GetTaskEntityByIdQuery(request.TaskId), cancellationToken);
            return mapper.Map<GetTaskDTO?>(taskEntity);
        }
    }
}
