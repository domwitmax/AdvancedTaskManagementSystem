using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddPriority;

public record AddPriorityCommand(string Name) : IRequest<int>;
