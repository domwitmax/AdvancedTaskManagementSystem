using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.UpdateContentComment;

public record UpdateContentCommentCommand(int CommentId, string Content) : IRequest<int?>;
