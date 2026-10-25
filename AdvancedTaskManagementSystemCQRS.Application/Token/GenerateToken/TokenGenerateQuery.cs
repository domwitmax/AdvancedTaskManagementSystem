using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Token.GenerateToken;

public record TokenGenerateQuery(GetAccountEntityDTO User) : IRequest<string>;