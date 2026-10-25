using AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.GetCommentById;

public record GetCommentByIdQuery(int CommentId) : IRequest<CommentDTO?>;
