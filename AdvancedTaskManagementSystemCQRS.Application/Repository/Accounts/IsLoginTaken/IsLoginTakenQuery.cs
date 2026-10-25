using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Accounts.IsLoginTaken;

public record IsLoginTakenQuery(string Login) : IRequest<bool>;