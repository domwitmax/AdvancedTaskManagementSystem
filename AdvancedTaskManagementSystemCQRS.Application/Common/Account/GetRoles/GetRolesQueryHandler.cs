using AdvancedTaskManagementSystemCQRS.Application.Common.Account.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetRoles;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.GetRoles;

public class GetRolesQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetRolesQuery, List<RoleDTO>>
{
    public async Task<List<RoleDTO>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await mediator.Send(new GetRoleEntitiesQuery(), cancellationToken);
        return mapper.Map<List<RoleDTO>>(roles);
    }
}
