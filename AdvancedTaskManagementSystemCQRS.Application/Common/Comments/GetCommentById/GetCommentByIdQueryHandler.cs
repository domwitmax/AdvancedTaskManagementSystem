using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.DTOs;
using AutoMapper;
using MediatR;
using RepositoryQuery = AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.GetCommentById.GetCommentByIdQuery;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.GetCommentById;

public class GetCommentByIdQueryHandler(IMediator mediator, IMapper mapper)
    : IRequestHandler<GetCommentByIdQuery, CommentDTO?>
{
    public async Task<CommentDTO?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var comment = await mediator.Send(new RepositoryQuery(request.CommentId), cancellationToken);
        return mapper.Map<CommentDTO?>(comment);
    }
}
