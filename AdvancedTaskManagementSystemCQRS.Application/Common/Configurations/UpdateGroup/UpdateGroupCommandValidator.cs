using FluentValidation;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateGroup
{
    public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator()
        {
            RuleFor(x => x.OldName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nazwa grupy jest wymagana.")
                .MaximumLength(100).WithMessage("Nazwa grupy może mieć maksymalnie 100 znaków.");
            RuleFor(x => x.NewName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nowa nazwa grupy jest wymagana.")
                .MaximumLength(100).WithMessage("Nowa nazwa grupy może mieć maksymalnie 100 znaków.");
        }
    }
}
