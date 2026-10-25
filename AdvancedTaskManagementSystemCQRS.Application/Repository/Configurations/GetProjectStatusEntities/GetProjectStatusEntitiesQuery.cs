using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetProjectStatusEntities;

public record GetProjectStatusEntitiesQuery() : IRequest<List<ProjectStatusEntityDTO>>;
