using AdvancedTaskManagementSystemCRUD.Application.Services.Comments.DTOs;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Comments.Validators;

public sealed class CommentDTOValidator : AbstractValidator<CommentDTO>
{
    public CommentDTOValidator()
    {
        RuleFor(x => x.Content)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Treść komentarza jest wymagana.")
            .MaximumLength(1000).WithMessage("Treść komentarza może mieć maksymalnie 1000 znaków.");
    }
}
