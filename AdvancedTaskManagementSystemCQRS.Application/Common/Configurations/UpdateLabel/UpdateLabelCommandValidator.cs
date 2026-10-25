using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetLabels;
using FluentValidation;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateLabel;

public class UpdateLabelCommandValidator : AbstractValidator<UpdateLabelCommand>
{
    public UpdateLabelCommandValidator(IMediator mediator)
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nazwa etykiety jest wymagana.")
            .MaximumLength(100).WithMessage("Nazwa etykiety może mieć maksymalnie 100 znaków.");

        RuleFor(x => x)
            .MustAsync(async (command, cancellation) =>
            {
                var labels = await mediator.Send(new GetLabelsQuery(), cancellation);
                return labels.All(label => label.Id == command.Id ||
                    !label.Name.Equals(command.Name, StringComparison.OrdinalIgnoreCase));
            }).WithMessage("Etykieta o podanej nazwie już istnieje.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Opis etykiety może mieć maksymalnie 500 znaków.");
    }
}
