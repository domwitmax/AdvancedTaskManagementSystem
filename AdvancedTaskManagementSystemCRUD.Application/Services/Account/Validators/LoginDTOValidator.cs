using AdvancedTaskManagementSystemCRUD.Application.Services.Account.DTOs;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Account.Validators;

public sealed class LoginDTOValidator : AbstractValidator<LoginDTO>
{
    public LoginDTOValidator()
    {
        RuleFor(x => x.Login)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Login jest wymagany.")
            .MaximumLength(50).WithMessage("Login może mieć maksymalnie 50 znaków.");

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Hasło jest wymagane.")
            .MinimumLength(6).WithMessage("Hasło musi mieć co najmniej 6 znaków.")
            .MaximumLength(100).WithMessage("Hasło może mieć maksymalnie 100 znaków.");
    }
}
