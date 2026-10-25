using AdvancedTaskManagementSystemCRUD.Application.Services.Configurations.DTOs;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Configurations.Validators
{
    public class GroupDTOValidator : AbstractValidator<GroupDTO>
    {
        public GroupDTOValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nazwa grupy jest wymagana.")
                .MaximumLength(100).WithMessage("Nazwa grupy może mieć maksymalnie 100 znaków.");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Opis grupy jest wymagany.")
                .MaximumLength(500).WithMessage("Opis grupy może mieć maksymalnie 500 znaków.");
        }
    }
}
