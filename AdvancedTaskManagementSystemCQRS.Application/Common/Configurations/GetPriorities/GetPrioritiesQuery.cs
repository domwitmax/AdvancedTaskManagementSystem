using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetPriorities;

public record GetPrioritiesQuery() : IRequest<List<PriorityDTO>>;
