using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeletePriority;

public record DeletePriorityEntityCommand(int Id) : IRequest<bool>;