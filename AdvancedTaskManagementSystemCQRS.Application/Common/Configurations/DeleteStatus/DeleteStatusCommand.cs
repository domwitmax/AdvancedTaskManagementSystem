using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteStatus;

public record DeleteStatusCommand(int Id) : IRequest<bool>;
