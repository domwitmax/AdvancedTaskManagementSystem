using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.DTOs;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.GetCommentsByTaskId;

public class GetCommentsByTaskIdQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetCommentsByTaskIdQuery, List<CommentDTO>>
{
    public async Task<List<CommentDTO>> Handle(GetCommentsByTaskIdQuery request, CancellationToken cancellationToken)
    {
        var comments = await mediator.Send(new AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.GetCommentsByTaskId.GetCommentsByTaskIdQuery(request.TaskId), cancellationToken);
        return mapper.Map<List<CommentDTO>>(comments);
    }
}
