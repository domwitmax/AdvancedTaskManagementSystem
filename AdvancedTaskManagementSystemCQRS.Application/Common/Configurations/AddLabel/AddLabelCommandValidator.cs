using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetLabels;
using FluentValidation;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddLabel;

public class AddLabelCommandValidator : AbstractValidator<AddLabelCommand>
{
    public AddLabelCommandValidator(IMediator mediator)
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nazwa etykiety jest wymagana.")
            .MaximumLength(100).WithMessage("Nazwa etykiety może mieć maksymalnie 100 znaków.")
            .MustAsync(async (name, cancellation) =>
            {
                var labels = await mediator.Send(new GetLabelsQuery(), cancellation);
                return labels.All(label => !label.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }).WithMessage("Etykieta o podanej nazwie już istnieje.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Opis etykiety może mieć maksymalnie 500 znaków.");
    }
}
