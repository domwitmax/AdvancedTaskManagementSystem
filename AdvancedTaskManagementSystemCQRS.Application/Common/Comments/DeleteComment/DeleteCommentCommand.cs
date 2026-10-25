using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.DeleteComment;

public record DeleteCommentCommand(int CommentId) : IRequest<bool>;
