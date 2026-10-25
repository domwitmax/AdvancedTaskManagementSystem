using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdatePriority;

public record UpdatePriorityCommand(int Id, string Name) : IRequest<int?>;
