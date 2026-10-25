using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetProjectEntitiesProgress;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Tasks.GetProjectEntitiesProgress;

public class GetProjectEntitiesProgressQueryHandler(TaskManagerDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetProjectEntitiesProgressQuery, ProjectProgressDTO?>
{
    public async Task<ProjectProgressDTO?> Handle(GetProjectEntitiesProgressQuery request, CancellationToken cancellationToken)
    {
        if (!await dbContext.Projects.AsNoTracking().AnyAsync(project => project.Id == request.ProjectId, cancellationToken))
            return null;

        var tasks = await dbContext.Task
            .AsNoTracking()
            .Include(item => item.Status)
            .Where(task => task.ProjectId == request.ProjectId)
            .ToListAsync(cancellationToken);

        return mapper.Map<ProjectProgressDTO>((
            ProjectId: request.ProjectId,
            AllTasks: tasks.Count,
            CompletedTasks: tasks.Count(task => task.Status.IsFinished)));
    }
}
