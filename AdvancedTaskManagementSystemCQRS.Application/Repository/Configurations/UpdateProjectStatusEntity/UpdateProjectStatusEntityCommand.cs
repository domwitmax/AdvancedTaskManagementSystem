using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateProjectStatusEntity;

public record UpdateProjectStatusEntityCommand(int Id, string Name) : IRequest<int?>;
