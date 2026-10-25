using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.UpdateAccount;

public record UpdateAccountCommonCommand(string Login, string Email) : IRequest<string?>;