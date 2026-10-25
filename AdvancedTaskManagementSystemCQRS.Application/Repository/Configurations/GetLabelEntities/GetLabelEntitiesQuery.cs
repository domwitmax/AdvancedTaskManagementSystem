using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetLabelEntities;

public record GetLabelEntitiesQuery() : IRequest<List<LabelEntityDTO>>;