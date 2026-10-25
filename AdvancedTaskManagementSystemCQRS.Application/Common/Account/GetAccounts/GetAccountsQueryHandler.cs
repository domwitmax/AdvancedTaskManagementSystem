using AdvancedTaskManagementSystemCQRS.Application.Common.Account.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccounts;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.GetAccounts;

public class GetAccountsQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetAccountsQuery, List<GetAccountCommonDTO>>
{
    public async Task<List<GetAccountCommonDTO>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await mediator.Send(new GetAccountEntitiesQuery(), cancellationToken);
        return mapper.Map<List<GetAccountCommonDTO>>(accounts);
    }
}
