using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Accounts.IsEmailTakenExcept;

public record IsEmailTakenExceptQuery(string Email, string Login) : IRequest<bool>;