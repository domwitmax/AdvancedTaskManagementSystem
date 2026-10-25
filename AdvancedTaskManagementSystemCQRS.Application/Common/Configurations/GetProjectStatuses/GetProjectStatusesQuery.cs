using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetProjectStatuses;

public record GetProjectStatusesQuery() : IRequest<List<ProjectStatusDTO>>;
