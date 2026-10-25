using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddLabelEntity;

public record AddLabelEntityCommand(string Name, string? Description) : IRequest<int>;
