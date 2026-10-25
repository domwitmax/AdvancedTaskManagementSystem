using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteProjectStatus;

public record DeleteProjectStatusCommand(int Id) : IRequest<bool>;
