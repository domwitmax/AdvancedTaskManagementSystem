using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateProjectStatus;

public record UpdateProjectStatusCommand(int Id, string Name) : IRequest<int?>;
