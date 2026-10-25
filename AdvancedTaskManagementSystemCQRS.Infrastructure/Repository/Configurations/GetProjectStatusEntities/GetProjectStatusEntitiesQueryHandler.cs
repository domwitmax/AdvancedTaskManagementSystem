using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetProjectStatusEntities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.GetProjectStatusEntities;

public class GetProjectStatusEntitiesQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetProjectStatusEntitiesQuery, List<ProjectStatusEntityDTO>>
{
    public async Task<List<ProjectStatusEntityDTO>> Handle(GetProjectStatusEntitiesQuery request, CancellationToken cancellationToken)
    {
        var statuses = await dbContext.ProjectStatuses.ToListAsync(cancellationToken);
        return mapper.Map<List<ProjectStatusEntityDTO>>(statuses);
    }
}
