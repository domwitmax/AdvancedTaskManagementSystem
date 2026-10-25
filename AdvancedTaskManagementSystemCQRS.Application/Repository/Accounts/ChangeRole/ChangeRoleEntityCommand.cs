using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Account.ChangeRole;

public record ChangeRoleEntityCommand(string Login, int RoleId) : IRequest<bool>;
