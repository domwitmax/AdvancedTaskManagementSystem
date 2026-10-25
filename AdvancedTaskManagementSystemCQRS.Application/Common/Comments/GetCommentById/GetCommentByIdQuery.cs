using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.GetCommentById;

public record GetCommentByIdQuery(int CommentId) : IRequest<CommentDTO?>;
