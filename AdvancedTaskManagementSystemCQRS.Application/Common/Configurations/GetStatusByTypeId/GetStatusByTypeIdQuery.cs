using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetStatusByTypeId;

public record GetStatusByTypeIdQuery(int TypeId) : IRequest<List<StatusDTO>>;
