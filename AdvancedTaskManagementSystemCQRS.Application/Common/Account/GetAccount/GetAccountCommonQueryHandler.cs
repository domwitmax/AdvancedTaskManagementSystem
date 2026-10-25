using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccount;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.GetAccount;

public class GetAccountCommonQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetAccountCommonQuery, GetAccountCommonDTO?>
{
    public async Task<GetAccountCommonDTO?> Handle(GetAccountCommonQuery request, CancellationToken cancellationToken)
    {
        var user = await mediator.Send(new GetAccountEntityQuery(request.UserName), cancellationToken);
        return mapper.Map<GetAccountCommonDTO>(user);
    }
}
