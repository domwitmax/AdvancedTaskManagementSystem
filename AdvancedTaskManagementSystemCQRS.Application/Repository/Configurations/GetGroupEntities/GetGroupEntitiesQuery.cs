using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetGroupEntities;

public record GetGroupEntitiesQuery() : IRequest<List<string>>;