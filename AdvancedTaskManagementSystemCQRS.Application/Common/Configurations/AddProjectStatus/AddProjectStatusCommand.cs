using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddProjectStatus;

public record AddProjectStatusCommand(string Name) : IRequest<int>;
