using AdvancedTaskManagementSystemCQRS.Application.Common.Account.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.GetActualAccount;

public record GetActualAccountCommonQuery(string UserName) : IRequest<GetActualAccountCommonDTO>;