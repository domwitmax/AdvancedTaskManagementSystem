using AdvancedTaskManagementSystemCQRS.Application.Common.ChangesHistory.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.ChangesHistory.GetChangesHistoryByTaskId;

public record GetChangesHistoryByTaskIdQuery(int TaskId) : IRequest<List<ChangesHistoryDTO>>;
