using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetDelayedTaskEntities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Tasks.GetDelayedTaskEntities
{
    public class GetDelayedTaskEntitiesQueryCommand(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetDelayedTaskEntitiesQuery, List<TaskEntityDTO>>
    {
        public async Task<List<TaskEntityDTO>> Handle(GetDelayedTaskEntitiesQuery request, CancellationToken cancellationToken)
        {
            var tasks = await dbContext.Task
                                .Include(t => t.Status)
                                .Include(t => t.Type)
                                .Include(t => t.Priority)
                                .Include(t => t.Project)
                                .Include(t => t.TaskLabels)
                                .ThenInclude(t => t.Label)
                                .Where(t => t.DueDate.HasValue && t.DueDate.Value < DateTime.UtcNow && !t.Status.IsFinished).ToListAsync();
            return mapper.Map<List<TaskEntityDTO>>(tasks);
        }
    }
}
