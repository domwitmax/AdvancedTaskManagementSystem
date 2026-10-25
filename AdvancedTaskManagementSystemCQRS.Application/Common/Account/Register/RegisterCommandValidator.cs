using AdvancedTaskManagementSystemCQRS.Application.Repository.Accounts.IsEmailTaken;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Accounts.IsLoginTaken;
using FluentValidation;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(IMediator mediator)
    {
        RuleFor(x => x.Login)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Login jest wymagany.")
            .MinimumLength(3).WithMessage("Login musi mieć co najmniej 3 znaki.")
            .MaximumLength(50).WithMessage("Login może mieć maksymalnie 50 znaków.")
            .MustAsync(async (login, cancellationToken) =>
            {
                var command = new IsLoginTakenQuery(login);
                return !(await mediator.Send(command, cancellationToken));
            })
            .WithMessage("Login jest już zajęty.");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email jest wymagany.")
            .EmailAddress().WithMessage("Email ma niepoprawny format.")
            .MaximumLength(100).WithMessage("Email może mieć maksymalnie 100 znaków.")
            .MustAsync(async (email, cancellationToken) =>
            {
                var command = new IsEmailTakenQuery(email);
                return !(await mediator.Send(command, cancellationToken));
            })
            .WithMessage("Email jest już zajęty.");

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Hasło jest wymagane.")
            .MinimumLength(6).WithMessage("Hasło musi mieć co najmniej 6 znaków.")
            .MaximumLength(100).WithMessage("Hasło może mieć maksymalnie 100 znaków.");
    }
}
