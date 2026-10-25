using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateStatus;

public record UpdateStatusCommand(int Id, string Name, bool IsFinished) : IRequest<int?>;
