using AdvancedTaskManagementSystemCRUD.WebApi.Request;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Validators
{
    public class UpdateGroupRequestValidator : AbstractValidator<UpdateGroupRequest>
    {
        public UpdateGroupRequestValidator()
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
