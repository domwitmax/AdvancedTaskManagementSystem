using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.History;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetLabelEntities;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetPriorityEntities;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetStatusEntityByTypeId;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetTypeEntities;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetProjectEntityById;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntityById;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.UpdateTaskEntity;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.EditTask
{
    public class EditTaskCommandHandler(IMediator mediator, IMapper mapper) : IRequestHandler<EditTaskCommand, int>
    {
        public async Task<int> Handle(EditTaskCommand request, CancellationToken cancellationToken)
        {
            var currentTask = await mediator.Send(new GetTaskEntityByIdQuery(request.Id), cancellationToken);
            if (currentTask == null)
                return 0;

            var types = await mediator.Send(new GetTypeEntitiesQuery(), cancellationToken);
            var type = types.FirstOrDefault(item => item.Id == request.TypeId);

            var statuses = await mediator.Send(new GetStatusEntityByTypeIdQuery(request.TypeId), cancellationToken);
            var status = statuses.FirstOrDefault(item => item.Id == request.StatusId);

            var project = await mediator.Send(new GetProjectEntityByIdQuery(request.ProjectId), cancellationToken);

            var priorities = await mediator.Send(new GetPriorityEntitiesQuery(), cancellationToken);
            var priority = priorities.FirstOrDefault(item => item.Id == request.PriorityId);

            var labels = await mediator.Send(new GetLabelEntitiesQuery(), cancellationToken);
            var requestedLabelIds = request.LabelIds?.Distinct().ToList() ?? new List<int>();
            var selectedLabels = labels
                .Where(item => requestedLabelIds.Contains(item.Id))
                .ToList();

            if (type == null || status == null || project == null || priority == null || selectedLabels.Count != requestedLabelIds.Count)
                return 0;

            var taskEntityDTO = mapper.Map<TaskEntityDTO>(request);
            taskEntityDTO.CreatedAt = currentTask.CreatedAt;
            taskEntityDTO.CreatedBy = currentTask.CreatedBy;
            taskEntityDTO.UpdatedAt = DateTime.UtcNow;

            var updateCommand = mapper.Map<UpdateTaskEntityCommand>(taskEntityDTO);
            updateCommand.ChangedBy = request.ChangedBy;
            updateCommand.ChangeDetails = TaskChangeDetailsBuilder.ForEdit(
                currentTask,
                request,
                type.Name,
                status.Name,
                project.Name,
                priority.Name,
                selectedLabels.Select(item => item.Name));

            return await mediator.Send(updateCommand, cancellationToken);
        }
    }
}
