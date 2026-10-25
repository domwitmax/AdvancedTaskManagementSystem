using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetPriorityEntities;

public record GetPriorityEntitiesQuery() : IRequest<List<PriorityEntityDTO>>;