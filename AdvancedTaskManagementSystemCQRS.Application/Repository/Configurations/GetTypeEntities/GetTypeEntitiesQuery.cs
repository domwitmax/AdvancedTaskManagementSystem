using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetTypeEntities;

public record GetTypeEntitiesQuery() : IRequest<List<TypeEntityDTO>>;