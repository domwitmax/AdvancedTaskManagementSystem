using AdvancedTaskManagementSystemCRUD.Application.Services.Account.DTOs;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Account.Validators;

public sealed class ChangePasswordDTOValidator : AbstractValidator<ChangePasswordDTO>
{
    public ChangePasswordDTOValidator()
    {
        RuleFor(x => x.Login)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Login jest wymagany.")
            .MaximumLength(50).WithMessage("Login może mieć maksymalnie 50 znaków.");

        RuleFor(x => x.OldPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Stare hasło jest wymagane.")
            .MaximumLength(100).WithMessage("Stare hasło może mieć maksymalnie 100 znaków.");

        RuleFor(x => x.NewPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nowe hasło jest wymagane.")
            .MinimumLength(6).WithMessage("Nowe hasło musi mieć co najmniej 6 znaków.")
            .MaximumLength(100).WithMessage("Nowe hasło może mieć maksymalnie 100 znaków.")
            .NotEqual(x => x.OldPassword).WithMessage("Nowe hasło musi być inne niż stare hasło.");
    }
}
