using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntityById;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Tasks.GetTaskEntityById
{
    public class GetTaskEntityByIdQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetTaskEntityByIdQuery, TaskEntityDTO?>
    {
        public async Task<TaskEntityDTO?> Handle(GetTaskEntityByIdQuery request, CancellationToken cancellationToken)
        {
            var taskEntity = await dbContext.Task
                .Include(item => item.Creator)
                .Include(item => item.Status)
                .Include(item => item.Type)
                .Include(item => item.Priority)
                .Include(item => item.Project)
                .Include(item => item.TaskLabels)
                    .ThenInclude(item => item.Label)
                .SingleOrDefaultAsync(item => item.Id == request.Id, cancellationToken);
            return mapper.Map<TaskEntityDTO?>(taskEntity);
        }
    }
}
