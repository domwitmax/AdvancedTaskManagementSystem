using AdvancedTaskManagementSystemCQRS.Application.Common.Account.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.GetRoles;

public record GetRolesQuery : IRequest<List<RoleDTO>>;
