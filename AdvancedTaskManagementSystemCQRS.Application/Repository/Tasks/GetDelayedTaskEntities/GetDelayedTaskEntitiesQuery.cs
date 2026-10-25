using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetDelayedTaskEntities;

public record GetDelayedTaskEntitiesQuery() : IRequest<List<TaskEntityDTO>>;