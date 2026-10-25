using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccount;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.GetActualAccount;

public class GetActualAccountCommonQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetActualAccountCommonQuery, GetActualAccountCommonDTO?>
{
    public async Task<GetActualAccountCommonDTO?> Handle(GetActualAccountCommonQuery request, CancellationToken cancellationToken)
    {
        var user = await mediator.Send(new GetAccountEntityQuery(request.UserName), cancellationToken);
        return mapper.Map<GetActualAccountCommonDTO>(user);
    }
}
