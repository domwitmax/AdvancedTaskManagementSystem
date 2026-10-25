using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetAllProjectEntities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Project.GetAllProjectEntities
{
    public class GetAllProjectEntitiesQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetAllProjectEntitiesQuery, List<ProjectEntityDTO>>
    {
        public async Task<List<ProjectEntityDTO>> Handle(GetAllProjectEntitiesQuery request, CancellationToken cancellationToken)
        {
            var projects = await dbContext.Projects.Include(p => p.Status).ToListAsync(cancellationToken);
            return mapper.Map<List<ProjectEntityDTO>>(projects);
        }
    }
}
