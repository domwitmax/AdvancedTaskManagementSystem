using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateLabel;

public record UpdateLabelCommand(int Id, string Name, string? Description) : IRequest<int?>;
