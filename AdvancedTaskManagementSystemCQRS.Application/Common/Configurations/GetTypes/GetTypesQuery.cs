using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetTypes;

public record GetTypesQuery() : IRequest<List<TypeDTO>>;
