using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateGroup;

public record UpdateGroupCommand(string OldName, string NewName) : IRequest<int?>;
