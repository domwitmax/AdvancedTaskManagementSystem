using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddStatus;

public record AddStatusCommand(string Name, int TypeId, bool IsFinished) : IRequest<int>;
