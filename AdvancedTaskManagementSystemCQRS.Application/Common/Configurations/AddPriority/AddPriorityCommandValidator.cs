using FluentValidation;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddPriority;

public sealed class AddPriorityCommandValidator : AbstractValidator<AddPriorityCommand>
{
    public AddPriorityCommandValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nazwa priorytetu jest wymagana.")
            .MaximumLength(100).WithMessage("Nazwa priorytetu może mieć maksymalnie 100 znaków.");
    }
}
