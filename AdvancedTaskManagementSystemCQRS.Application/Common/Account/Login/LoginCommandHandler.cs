using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccount;
using AdvancedTaskManagementSystemCQRS.Application.PasswordHasher.Verify;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.UpdateLastLogin;
using AdvancedTaskManagementSystemCQRS.Application.Token.GenerateToken;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.Login;

public class LoginCommandHandler(IMediator mediator) : IRequestHandler<LoginCommand, string?>
{
    public async Task<string?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await mediator.Send(new GetAccountEntityQuery(request.Login), cancellationToken);
        if (user == null || !user.Active)
            return null;

        var verificationResult = await mediator.Send(
            new PasswordHasherVerifyQuery(request.Password, user.PasswordHash),
            cancellationToken);

        if (!verificationResult)
            return null;

        await mediator.Send(new UpdateLastLoginEntityCommand(user.Login), cancellationToken);
        return await mediator.Send(new TokenGenerateQuery(user), cancellationToken);
    }
}
