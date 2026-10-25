using AdvancedTaskManagementSystemCQRS.Application.Repository.Accounts.IsEmailTakenExcept;
using FluentValidation;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.UpdateAccount;

public sealed class UpdateAccountCommonCommandValidator : AbstractValidator<UpdateAccountCommonCommand>
{
    public UpdateAccountCommonCommandValidator(IMediator mediator)
    {
        RuleFor(x => x.Login)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Login jest wymagany.")
            .MaximumLength(50).WithMessage("Login może mieć maksymalnie 50 znaków.");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email jest wymagany.")
            .EmailAddress().WithMessage("Email ma niepoprawny format.")
            .MaximumLength(100).WithMessage("Email może mieć maksymalnie 100 znaków.");

        RuleFor(x => x)
            .MustAsync(async (command, cancellation) =>
            {
                var queryCommand = new IsEmailTakenExceptQuery(command.Email, command.Login);
                return !await mediator.Send(queryCommand, cancellation);
            }).WithMessage("Email jest już zajęty przez innego użytkownika.");
    }
}
