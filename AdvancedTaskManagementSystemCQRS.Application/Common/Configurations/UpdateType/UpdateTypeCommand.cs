using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateType;

public record UpdateTypeCommand(int Id, string Name) : IRequest<int?>;
