using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccount;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntityById;
using FluentValidation;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.AddTaskAttachment;

public class AddTaskAttachmentCommandValidator : AbstractValidator<AddTaskAttachmentCommand>
{
    public AddTaskAttachmentCommandValidator(IMediator mediator)
    {
        RuleFor(x => x.TaskId)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Id zadania jest wymagane.")
            .MustAsync(async (taskId, cancellationToken) =>
                await mediator.Send(new GetTaskEntityByIdQuery(taskId), cancellationToken) != null)
            .WithMessage("Zadanie nie istnieje.");

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("Nazwa pliku jest wymagana.")
            .MaximumLength(255).WithMessage("Nazwa pliku może mieć maksymalnie 255 znaków.");

        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage("Typ pliku jest wymagany.")
            .MaximumLength(200).WithMessage("Typ pliku może mieć maksymalnie 200 znaków.");

        RuleFor(x => x.FileSize)
            .GreaterThan(0).WithMessage("Plik nie może być pusty.")
            .LessThanOrEqualTo(10 * 1024 * 1024).WithMessage("Maksymalny rozmiar pliku to 10 MB.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Zawartość pliku jest wymagana.");

        RuleFor(x => x.UploadedBy)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Użytkownik dodający załącznik jest wymagany.")
            .MaximumLength(50).WithMessage("Login użytkownika może mieć maksymalnie 50 znaków.")
            .MustAsync(async (uploadedBy, cancellationToken) =>
            {
                var account = await mediator.Send(new GetAccountEntityQuery(uploadedBy), cancellationToken);
                return account?.Active == true;
            })
            .WithMessage("Użytkownik dodający załącznik nie istnieje albo jest nieaktywny.");
    }
}
