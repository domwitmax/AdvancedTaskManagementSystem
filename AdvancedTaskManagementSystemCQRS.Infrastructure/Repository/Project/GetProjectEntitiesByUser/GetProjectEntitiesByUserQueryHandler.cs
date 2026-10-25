using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetProjectEntitiesByUser;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Project.GetProjectEntitiesByUser;

public class GetProjectEntitiesByUserQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetProjectEntitiesByUserQuery, List<ProjectEntityDTO>>
{
    public async Task<List<ProjectEntityDTO>> Handle(GetProjectEntitiesByUserQuery request, CancellationToken cancellationToken)
    {
        var projects = await dbContext.Projects
            .AsNoTracking()
            .Include(item => item.Status)
            .Where(item => item.ProjectMembers.Any(member => member.AccountLogin == request.UserName))
            .OrderBy(item => item.Name)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<ProjectEntityDTO>>(projects);
    }
}
