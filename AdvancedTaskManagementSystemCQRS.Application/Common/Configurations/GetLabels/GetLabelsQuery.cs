using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetLabels;

public record GetLabelsQuery : IRequest<List<LabelDTO>>;