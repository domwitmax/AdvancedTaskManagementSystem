using AdvancedTaskManagementSystemCQRS.Application.Repository.ChangesHistory.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.ChangesHistory.GetChangesHistoryEntitiesByTaskId;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.ChangesHistory.GetChangesHistoryEntitiesByTaskId;

public class GetChangesHistoryEntitiesByTaskIdQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetChangesHistoryEntitiesByTaskIdQuery, List<ChangesHistoryEntityDTO>>
{
    public async Task<List<ChangesHistoryEntityDTO>> Handle(GetChangesHistoryEntitiesByTaskIdQuery request, CancellationToken cancellationToken)
    {
        var history = await dbContext.ChangesHistory
            .Include(ch => ch.ChangeDetails)
            .Where(ch => ch.TaskId == request.TaskId)
            .OrderByDescending(ch => ch.ChangedAt)
            .ThenByDescending(ch => ch.Id)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<ChangesHistoryEntityDTO>>(history);
    }
}
