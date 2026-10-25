using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccount;

public record GetAccountEntityQuery(string UserName) : IRequest<GetAccountEntityDTO>;