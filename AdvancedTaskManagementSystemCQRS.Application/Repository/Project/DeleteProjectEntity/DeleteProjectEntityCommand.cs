using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DeleteProjectEntity;

public record DeleteProjectEntityCommand(int ProjectId) : IRequest<int>;
