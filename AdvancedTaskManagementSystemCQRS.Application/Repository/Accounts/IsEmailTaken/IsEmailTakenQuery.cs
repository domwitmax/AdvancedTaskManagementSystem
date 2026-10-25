using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Accounts.IsEmailTaken;

public record IsEmailTakenQuery(string Email) : IRequest<bool>;
