using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Services.Account.DTOs;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Account.Validators;

public sealed class RegisterDTOValidator : AbstractValidator<RegisterDTO>
{
    public RegisterDTOValidator(IAccountRepository accountRepository)
    {
        RuleFor(x => x.Login)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Login jest wymagany.")
            .MinimumLength(3).WithMessage("Login musi mieć co najmniej 3 znaki.")
            .MaximumLength(50).WithMessage("Login może mieć maksymalnie 50 znaków.")
            .Must(login =>
            {
                return !accountRepository.IsLoginTaken(login);
            })
            .WithMessage("Login jest już zajęty.");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email jest wymagany.")
            .EmailAddress().WithMessage("Email ma niepoprawny format.")
            .MaximumLength(100).WithMessage("Email może mieć maksymalnie 100 znaków.")
            .Must(email =>
            {
                return !accountRepository.IsEmailTaken(email);
            })
            .WithMessage("Email jest już zajęty.");

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Hasło jest wymagane.")
            .MinimumLength(6).WithMessage("Hasło musi mieć co najmniej 6 znaków.")
            .MaximumLength(100).WithMessage("Hasło może mieć maksymalnie 100 znaków.");
    }
}
