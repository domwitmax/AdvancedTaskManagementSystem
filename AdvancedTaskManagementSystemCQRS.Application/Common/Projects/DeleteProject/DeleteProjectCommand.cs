using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DeleteProject;

public record DeleteProjectCommand(int ProjectId) : IRequest<int>;
