using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetProjectMemberEntities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Project.GetProjectMemberEntities;

public class GetProjectMemberEntitiesQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetProjectMemberEntitiesQuery, List<ProjectMemberEntityDTO>>
{
    public async Task<List<ProjectMemberEntityDTO>> Handle(GetProjectMemberEntitiesQuery request, CancellationToken cancellationToken)
    {
        var members = await dbContext.ProjectMembers
            .AsNoTracking()
            .Include(item => item.Account)
            .ThenInclude(item => item.Role)
            .Where(item => item.ProjectId == request.ProjectId)
            .OrderBy(item => item.AccountLogin)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<ProjectMemberEntityDTO>>(members);
    }
}
