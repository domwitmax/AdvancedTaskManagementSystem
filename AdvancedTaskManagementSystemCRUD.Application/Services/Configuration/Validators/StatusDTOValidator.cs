using AdvancedTaskManagementSystemCRUD.Application.Services.Configurations.DTOs;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Configuration.Validators
{
    public class StatusDTOValidator : AbstractValidator<StatusDTO>
    {
        public StatusDTOValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nazwa statusu jest wymagana.")
                .MaximumLength(100).WithMessage("Nazwa statusu może mieć maksymalnie 100 znaków.");
        }
    }
}
