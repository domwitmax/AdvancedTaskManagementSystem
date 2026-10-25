using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteProjectStatusEntity;

public record DeleteProjectStatusEntityCommand(int Id) : IRequest<bool>;
