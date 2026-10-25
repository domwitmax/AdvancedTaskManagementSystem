using FluentValidation;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateType
{
    public class UpdateTypeCommandValidator : AbstractValidator<UpdateTypeCommand>
    {
        public UpdateTypeCommandValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nazwa typu jest wymagana.")
                .MaximumLength(100).WithMessage("Nazwa typu może mieć maksymalnie 100 znaków.");
        }
    }
}
