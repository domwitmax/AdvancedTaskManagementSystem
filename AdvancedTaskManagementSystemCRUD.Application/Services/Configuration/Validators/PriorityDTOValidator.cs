using AdvancedTaskManagementSystemCRUD.Application.Services.Configurations.DTOs;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Configuration.Validators
{
    public class PriorityDTOValidator : AbstractValidator<PriorityDTO>
    {
        public PriorityDTOValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nazwa priorytetu jest wymagana.")
                .MaximumLength(100).WithMessage("Nazwa priorytetu może mieć maksymalnie 100 znaków.");
        }
    }
}
