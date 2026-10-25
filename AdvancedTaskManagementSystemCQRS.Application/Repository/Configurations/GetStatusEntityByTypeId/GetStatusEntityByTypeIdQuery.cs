using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetStatusEntityByTypeId;

public record GetStatusEntityByTypeIdQuery(int typeId) : IRequest<List<StatusEntityDTO>>;