using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.ChangeRole;

public record ChangeRoleCommand(string Login, int RoleId) : IRequest<bool>;
