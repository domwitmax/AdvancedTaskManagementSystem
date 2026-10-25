using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.UpdateProject;

public record UpdateProjectCommand(int Id, string Name, string Description, int StatusId, DateTime StartDate, DateTime? EndDate) : IRequest<int?>;
