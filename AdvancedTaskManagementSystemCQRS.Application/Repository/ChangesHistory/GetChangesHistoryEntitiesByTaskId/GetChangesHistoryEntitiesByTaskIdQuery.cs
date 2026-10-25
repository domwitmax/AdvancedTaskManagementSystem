using AdvancedTaskManagementSystemCQRS.Application.Repository.ChangesHistory.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.ChangesHistory.GetChangesHistoryEntitiesByTaskId;

public record GetChangesHistoryEntitiesByTaskIdQuery(int TaskId) : IRequest<List<ChangesHistoryEntityDTO>>;
