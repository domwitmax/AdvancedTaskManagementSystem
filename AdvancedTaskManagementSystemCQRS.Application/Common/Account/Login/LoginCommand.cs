using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.Login;

public record LoginCommand(string Login, string Password) : IRequest<string?>;