using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.UpdateContentComment;

public record UpdateContentCommentCommand(int CommentId, string Content) : IRequest<int?>;
