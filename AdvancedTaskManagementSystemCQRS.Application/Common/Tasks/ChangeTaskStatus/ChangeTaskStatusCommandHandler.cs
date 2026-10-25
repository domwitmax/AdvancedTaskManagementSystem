using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.History;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetStatusEntityByTypeId;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntityById;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.UpdateTaskEntity;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.ChangeTaskStatus
{
    public class ChangeTaskStatusCommandHandler(IMediator mediator, IMapper mapper) : IRequestHandler<ChangeTaskStatusCommand, int>
    {
        public async Task<int> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var taskEntity = await mediator.Send(new GetTaskEntityByIdQuery(request.Id), cancellationToken);
            if (taskEntity == null)
                return 0;

            var statuses = await mediator.Send(new GetStatusEntityByTypeIdQuery(taskEntity.TypeId), cancellationToken);
            var status = statuses.FirstOrDefault(item => item.Id == request.StatusId);
            if (status == null)
                return 0;

            var oldStatus = taskEntity.Status;
            taskEntity.StatusId = request.StatusId;

            var updateCommand = mapper.Map<UpdateTaskEntityCommand>(taskEntity);
            updateCommand.ChangedBy = request.ChangedBy;
            updateCommand.ChangeDetails = TaskChangeDetailsBuilder.ForStatus(oldStatus, status.Name);

            return await mediator.Send(updateCommand, cancellationToken);
        }
    }
}
