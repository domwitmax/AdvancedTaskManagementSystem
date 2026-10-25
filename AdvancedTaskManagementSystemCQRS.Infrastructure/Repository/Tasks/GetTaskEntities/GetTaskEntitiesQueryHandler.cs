using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Tasks.GetTaskEntities
{
    public class GetTaskEntitiesQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetTaskEntitiesQuery, List<TaskEntityDTO>>
    {
        public async Task<List<TaskEntityDTO>> Handle(GetTaskEntitiesQuery request, CancellationToken cancellationToken)
        {  
            var taskEntities = await dbContext.Task
                .Include(item => item.Status)
                .Include(item => item.Type)
                .Include(item => item.Priority)
                .Include(item => item.Project)
                .Include(item => item.TaskLabels)
                    .ThenInclude(item => item.Label)
                .ToListAsync(cancellationToken);
            return mapper.Map<List<TaskEntityDTO>>(taskEntities);   
        }
    }
}
