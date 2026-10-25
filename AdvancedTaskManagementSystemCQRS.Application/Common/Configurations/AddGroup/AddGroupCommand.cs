using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddGroup;

public record AddGroupCommand(string Name, string Description) : IRequest<int>;
