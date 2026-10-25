using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateLabelEntity;

public record UpdateLabelEntityCommand(int Id, string Name, string? Description) : IRequest<int?>;
