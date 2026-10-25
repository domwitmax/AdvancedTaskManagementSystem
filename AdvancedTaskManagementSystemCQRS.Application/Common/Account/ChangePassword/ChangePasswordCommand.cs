using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.ChangePassword;

public record ChangePasswordCommand(string Login, string OldPassword, string NewPassword) : IRequest<string?>;