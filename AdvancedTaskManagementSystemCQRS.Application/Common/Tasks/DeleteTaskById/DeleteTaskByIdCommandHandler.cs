using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DeleteTaskEntity;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntityById;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DeleteTaskById
{
    public class DeleteTaskByIdCommandHandler(IMediator mediator) : IRequestHandler<DeleteTaskByIdCommand, bool>
    {
        public async Task<bool> Handle(DeleteTaskByIdCommand request, CancellationToken cancellationToken)
        {
            var taskEntity = await mediator.Send(new GetTaskEntityByIdQuery(request.Id), cancellationToken);
            if(taskEntity == null)
            {
                return false;
            }
            return await mediator.Send(new DeleteTaskEntityCommand(request.Id), cancellationToken);
        }
    }
}
