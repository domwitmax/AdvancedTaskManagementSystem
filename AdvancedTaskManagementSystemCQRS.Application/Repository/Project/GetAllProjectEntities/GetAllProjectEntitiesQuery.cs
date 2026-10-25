using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetAllProjectEntities;

public record GetAllProjectEntitiesQuery() : IRequest<List<ProjectEntityDTO>>;