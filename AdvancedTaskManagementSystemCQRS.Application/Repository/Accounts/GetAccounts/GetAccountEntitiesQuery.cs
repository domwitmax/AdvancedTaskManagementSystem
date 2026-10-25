using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccounts;

public record GetAccountEntitiesQuery : IRequest<List<GetAccountEntityDTO>>;
