using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccount;
using AdvancedTaskManagementSystemCQRS.Application.PasswordHasher.Hash;
using AdvancedTaskManagementSystemCQRS.Application.PasswordHasher.Verify;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.UpdateAccount;
using AdvancedTaskManagementSystemCQRS.Application.Token.GenerateToken;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.ChangePassword;

public class ChangePasswordCommandHandler(IMediator mediator, IMapper mapper) : IRequestHandler<ChangePasswordCommand, string?>
{
    public async Task<string?> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await mediator.Send(new GetAccountEntityQuery(request.Login));
        if (user == null)
            return null;

        var verify = await mediator.Send(new PasswordHasherVerifyQuery(request.OldPassword, user.PasswordHash));

        if (!verify)
            return null;

        user.PasswordHash = await mediator.Send(new PasswordHasherHashQuery(request.NewPassword));
        user.ChangePasswordAt = DateTime.UtcNow;

        var updateAccountCommand = mapper.Map<UpdateAccountEntityCommand>(user);
        await mediator.Send(updateAccountCommand);

        return await mediator.Send(new TokenGenerateQuery(user));
    }
}
