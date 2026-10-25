using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddProjectStatusEntity;

public record AddProjectStatusEntityCommand(string Name) : IRequest<int>;
