using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccount;
using AdvancedTaskManagementSystemCQRS.Application.PasswordHasher.Hash;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.AddAccount;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Token.GenerateToken;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.Register;

public class RegisterCommandHandler(IMediator mediator, IMapper mapper) : IRequestHandler<RegisterCommand, string?>
{
    public async Task<string?> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = await mediator.Send(new PasswordHasherHashQuery(request.Password), cancellationToken);
        var addAccountCommand = mapper.Map<AddAccountEntityCommand>(request);
        addAccountCommand.PasswordHash = passwordHash;

        await mediator.Send(addAccountCommand, cancellationToken);

        var user = await mediator.Send(new GetAccountEntityQuery(request.Login), cancellationToken);
        if (user == null)
            return null;

        return await mediator.Send(new TokenGenerateQuery(user), cancellationToken);
    }
}
