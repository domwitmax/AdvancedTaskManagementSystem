using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.History;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntityById;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.UpdateTaskEntity;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.AssignmentTask
{
    public class AssignmentTaskCommandHandler(IMediator mediator, IMapper mapper) : IRequestHandler<AssignmentTaskCommand, int>
    {
        public async Task<int> Handle(AssignmentTaskCommand request, CancellationToken cancellationToken)
        {
            var taskEntity = await mediator.Send(new GetTaskEntityByIdQuery(request.Id), cancellationToken);
            if (taskEntity == null)
                return 0;

            var oldAssignedTo = taskEntity.AssignedTo;
            taskEntity.AssignedTo = request.AssignedTo;

            var updateCommand = mapper.Map<UpdateTaskEntityCommand>(taskEntity);
            updateCommand.ChangedBy = request.ChangedBy;
            updateCommand.ChangeDetails = TaskChangeDetailsBuilder.ForAssignment(oldAssignedTo, request.AssignedTo);

            return await mediator.Send(updateCommand, cancellationToken);
        }
    }
}
