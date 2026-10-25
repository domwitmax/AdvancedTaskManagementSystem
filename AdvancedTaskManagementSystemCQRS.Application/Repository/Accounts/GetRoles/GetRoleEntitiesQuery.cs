using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetRoles;

public record GetRoleEntitiesQuery : IRequest<List<RoleEntityDTO>>;
