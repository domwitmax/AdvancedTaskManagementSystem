using AdvancedTaskManagementSystemCQRS.Application.Common.ChangesHistory.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.ChangesHistory.GetChangesHistoryEntitiesByTaskId;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.ChangesHistory.GetChangesHistoryByTaskId;

public class GetChangesHistoryByTaskIdQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetChangesHistoryByTaskIdQuery, List<ChangesHistoryDTO>>
{
    public async Task<List<ChangesHistoryDTO>> Handle(GetChangesHistoryByTaskIdQuery request, CancellationToken cancellationToken)
    {
        var history = await mediator.Send(new GetChangesHistoryEntitiesByTaskIdQuery(request.TaskId), cancellationToken);
        return mapper.Map<List<ChangesHistoryDTO>>(history);
    }
}
