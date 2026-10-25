using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.Register;

public record RegisterCommand(string Login, string Email, string Password) : IRequest<string?>;
