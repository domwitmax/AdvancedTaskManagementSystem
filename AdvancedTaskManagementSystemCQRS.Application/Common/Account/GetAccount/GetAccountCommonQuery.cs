using AdvancedTaskManagementSystemCQRS.Application.Common.Account.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.GetAccount;

public record GetAccountCommonQuery(string UserName) : IRequest<GetAccountCommonDTO>;