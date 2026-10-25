using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetTasksByStatus;

public record GetTasksByStatusQuery() : IRequest<List<TasksByStatusDTO>>;
