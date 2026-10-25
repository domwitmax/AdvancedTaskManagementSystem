using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Services.Account.DTOs;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Account.Validators;

public sealed class UpdateAccountDTOValidator : AbstractValidator<UpdateAccountDTO>
{
    public UpdateAccountDTOValidator(IAccountRepository accountRepository)
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
            .Must((command, cancellation) =>
            {
                return !accountRepository.IsEmailTakenExcept(command.Email, command.Login);
            }).WithMessage("Email jest już zajęty przez innego użytkownika.");
    }
}
