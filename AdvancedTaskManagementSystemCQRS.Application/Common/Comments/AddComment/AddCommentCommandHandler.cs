using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.AddComment;

public class AddCommentCommandHandler(IMediator mediator) : IRequestHandler<AddCommentCommand, int?>
{
    public async Task<int?> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new Repository.Comments.AddComment.AddCommentCommand(request.TaskId, request.Content, request.CreatedBy), cancellationToken);
    }
}
