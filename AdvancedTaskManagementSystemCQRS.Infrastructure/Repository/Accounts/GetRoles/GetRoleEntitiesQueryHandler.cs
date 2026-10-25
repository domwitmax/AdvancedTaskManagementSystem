using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetRoles;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Account.GetRoles;

public class GetRoleEntitiesQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetRoleEntitiesQuery, List<RoleEntityDTO>>
{
    public async Task<List<RoleEntityDTO>> Handle(GetRoleEntitiesQuery request, CancellationToken cancellationToken)
    {
        var roles = await dbContext.Roles.AsNoTracking().OrderBy(item => item.Id).ToListAsync(cancellationToken);
        return mapper.Map<List<RoleEntityDTO>>(roles);
    }
}
