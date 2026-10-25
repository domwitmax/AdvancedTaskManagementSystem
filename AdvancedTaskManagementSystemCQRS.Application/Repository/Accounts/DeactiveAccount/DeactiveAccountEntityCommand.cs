using AdvancedTaskManagementSystemCQRS.Application.Interfaces;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DeactiveAccount;

public record DeactiveAccountEntityCommand(string UserName) : IRequest<bool>, IDbCommand;