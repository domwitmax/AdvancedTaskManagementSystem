using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddType;

public record AddTypeCommand(string Name) : IRequest<int>;
