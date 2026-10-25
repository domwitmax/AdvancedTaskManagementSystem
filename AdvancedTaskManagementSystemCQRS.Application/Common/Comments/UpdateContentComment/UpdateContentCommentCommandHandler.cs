using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.UpdateContentComment;

public class UpdateContentCommentCommandHandler(IMediator mediator) : IRequestHandler<UpdateContentCommentCommand, int?>
{
    public async Task<int?> Handle(UpdateContentCommentCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new Repository.Comments.UpdateContentComment.UpdateContentCommentCommand(request.CommentId, request.Content), cancellationToken);
    }
}
