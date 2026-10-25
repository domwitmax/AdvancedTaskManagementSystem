using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteGroup;

public record DeleteGroupCommand(string Name) : IRequest<bool>;
