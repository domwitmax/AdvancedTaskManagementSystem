using AdvancedTaskManagementSystemCQRS.Application.Common.Account.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.GetAccounts;

public record GetAccountsQuery : IRequest<List<GetAccountCommonDTO>>;
