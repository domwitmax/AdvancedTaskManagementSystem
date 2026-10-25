using AdvancedTaskManagementSystemCRUD.Application.Services.Configurations.DTOs;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Configuration.Validators
{
    public class TypeDTOValidator : AbstractValidator<TypeDTO>
    {
        public TypeDTOValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nazwa typu jest wymagana.")
                .MaximumLength(100).WithMessage("Nazwa typu może mieć maksymalnie 100 znaków.");
        }
    }
}
