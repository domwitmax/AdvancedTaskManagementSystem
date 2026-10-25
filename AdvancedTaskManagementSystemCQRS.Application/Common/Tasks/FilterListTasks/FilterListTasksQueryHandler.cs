using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntities;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.FilterListTasks
{
    public class FilterListTasksQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<FilterListTasksQuery, List<GetTaskDTO>>
    {
        public async Task<List<GetTaskDTO>> Handle(FilterListTasksQuery request, CancellationToken cancellationToken)
        {
            var filteredTasks = await mediator.Send(new GetTaskEntitiesQuery(), cancellationToken);

            if (request.ByTypeId != null)
            {
                filteredTasks = filteredTasks.Where(t => t.TypeId == request.ByTypeId).ToList();
            }

            if (request.ByStatusId != null)
            {
                filteredTasks = filteredTasks.Where(t => t.StatusId == request.ByStatusId).ToList();
            }

            if (!string.IsNullOrEmpty(request.ByPriority))
            {
                filteredTasks = filteredTasks.Where(t => t.Priority.Equals(request.ByPriority, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(request.ByCreator))
            {
                filteredTasks = filteredTasks.Where(t => t.CreatedBy.Equals(request.ByCreator, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            if (!string.IsNullOrEmpty(request.ByAssignedTo))
            {
                filteredTasks = filteredTasks.Where(t => t.AssignedTo.Equals(request.ByAssignedTo, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(request.ByLabel))
            {
                filteredTasks = filteredTasks.Where(t => t.Labels.Any(label => label.Equals(request.ByLabel, StringComparison.OrdinalIgnoreCase))).ToList();
            }

            if (request.BetweenCreatedBegin != null)
            {
                filteredTasks = filteredTasks.Where(t => t.CreatedAt >= request.BetweenCreatedBegin).ToList();
            }

            if (request.BetweenCreatedEnd != null)
            {
                filteredTasks = filteredTasks.Where(t => t.CreatedAt <= request.BetweenCreatedEnd).ToList();
            }

            if (request.BetweenUpdatedBegin != null)
            {
                filteredTasks = filteredTasks.Where(t => t.UpdatedAt.HasValue && t.UpdatedAt.Value >= request.BetweenUpdatedBegin).ToList();
            }
            if (request.BetweenUpdatedEnd != null)
            {
                filteredTasks = filteredTasks.Where(t => t.UpdatedAt.HasValue && t.UpdatedAt.Value <= request.BetweenUpdatedEnd).ToList();
            }

            if (request.BetweenDueDateBegin != null)
            {
                filteredTasks = filteredTasks.Where(t => t.DueDate.HasValue && t.DueDate.Value >= request.BetweenDueDateBegin).ToList();
            }

            if (request.BetweenDueDateEnd != null)
            {
                filteredTasks = filteredTasks.Where(t => t.DueDate.HasValue && t.DueDate.Value <= request.BetweenDueDateEnd).ToList();
            }

            filteredTasks = SortTasks(filteredTasks, request.SortBy, request.SortDescending);

            return mapper.Map<List<GetTaskDTO>>(filteredTasks);
        }

        private static List<TaskEntityDTO> SortTasks(
            List<TaskEntityDTO> tasks,
            string? sortBy,
            bool sortDescending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                return tasks;
            }

            return sortBy.Trim().ToLowerInvariant() switch
            {
                "title" => sortDescending ? tasks.OrderByDescending(item => item.Title).ToList() : tasks.OrderBy(item => item.Title).ToList(),
                "type" => sortDescending ? tasks.OrderByDescending(item => item.Type).ToList() : tasks.OrderBy(item => item.Type).ToList(),
                "status" => sortDescending ? tasks.OrderByDescending(item => item.Status).ToList() : tasks.OrderBy(item => item.Status).ToList(),
                "project" => sortDescending ? tasks.OrderByDescending(item => item.ProjectName).ToList() : tasks.OrderBy(item => item.ProjectName).ToList(),
                "priority" => sortDescending ? tasks.OrderByDescending(item => item.Priority).ToList() : tasks.OrderBy(item => item.Priority).ToList(),
                "duedate" => sortDescending ? tasks.OrderByDescending(item => item.DueDate).ToList() : tasks.OrderBy(item => item.DueDate).ToList(),
                "createdat" => sortDescending ? tasks.OrderByDescending(item => item.CreatedAt).ToList() : tasks.OrderBy(item => item.CreatedAt).ToList(),
                "updatedat" => sortDescending ? tasks.OrderByDescending(item => item.UpdatedAt).ToList() : tasks.OrderBy(item => item.UpdatedAt).ToList(),
                "assignedto" => sortDescending ? tasks.OrderByDescending(item => item.AssignedTo).ToList() : tasks.OrderBy(item => item.AssignedTo).ToList(),
                "createdby" => sortDescending ? tasks.OrderByDescending(item => item.CreatedBy).ToList() : tasks.OrderBy(item => item.CreatedBy).ToList(),
                _ => tasks
            };
        }
    }
}
