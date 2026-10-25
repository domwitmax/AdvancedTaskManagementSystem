using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Account.UpdateLastLogin;

public record UpdateLastLoginEntityCommand(string Login) : IRequest<bool>;
