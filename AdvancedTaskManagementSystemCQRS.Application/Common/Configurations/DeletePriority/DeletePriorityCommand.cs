using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeletePriority;

public record DeletePriorityCommand(int Id) : IRequest<bool>;
