using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.AddComment;

public record AddCommentCommand(int TaskId, string Content, string CreatedBy) : IRequest<int?>;