using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configurations.DTOs;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Configuration.Validators
{
    public class LabelDTOValidator : AbstractValidator<LabelDTO>
    {
        public LabelDTOValidator(IConfigurationRepository configurationRepository)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nazwa etykiety jest wymagana.")
                .MaximumLength(100).WithMessage("Nazwa etykiety może mieć maksymalnie 100 znaków.");

            RuleFor(x => x)
                .Must(label => configurationRepository.GetLabels().All(existing =>
                    existing.Id == label.Id || !existing.Name.Equals(label.Name, StringComparison.OrdinalIgnoreCase)))
                .WithMessage("Etykieta o podanej nazwie już istnieje.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Opis etykiety może mieć maksymalnie 500 znaków.");
        }
    }
}
