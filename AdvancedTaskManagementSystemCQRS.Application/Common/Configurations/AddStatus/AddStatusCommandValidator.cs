using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetTypeEntities;
using FluentValidation;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddStatus;

public sealed class AddStatusCommandValidator : AbstractValidator<AddStatusCommand>
{
    public AddStatusCommandValidator(IMediator mediator)
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nazwa statusu jest wymagana.")
            .MaximumLength(100).WithMessage("Nazwa statusu może mieć maksymalnie 100 znaków.");

        RuleFor(x => x)
            .MustAsync(async (command, cancellation) =>
            {
                var typres = await mediator.Send(new GetTypeEntitiesQuery(), cancellation);
                return typres.Any(item => item.Id == command.TypeId);
            })
            .WithMessage("Typ statusu nie instnieje");
    }
}
