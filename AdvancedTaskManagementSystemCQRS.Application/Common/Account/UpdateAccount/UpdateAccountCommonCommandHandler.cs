using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccount;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.UpdateAccount;
using AdvancedTaskManagementSystemCQRS.Application.Token.GenerateToken;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.UpdateAccount
{
    public class UpdateAccountCommonCommandHandler(IMediator mediator, IMapper mapper) : IRequestHandler<UpdateAccountCommonCommand, string?>
    {
        public async Task<string?> Handle(UpdateAccountCommonCommand request, CancellationToken cancellationToken)
        {
            var userFromDb = await mediator.Send(new GetAccountEntityQuery(request.Login ?? string.Empty), cancellationToken);
            if (userFromDb == null)
                return null;
            await mediator.Send(mapper.Map<UpdateAccountEntityCommand>(request), cancellationToken);
            var userDTO = await mediator.Send(new GetAccountEntityQuery(request.Login ?? string.Empty), cancellationToken);
            if (userDTO == null)
                return null;

            return await mediator.Send(new TokenGenerateQuery(userDTO), cancellationToken);
        }
    }
}
