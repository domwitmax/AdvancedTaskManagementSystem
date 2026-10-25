using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddLabel;

public record AddLabelCommand(string Name, string? Description) : IRequest<int>;
