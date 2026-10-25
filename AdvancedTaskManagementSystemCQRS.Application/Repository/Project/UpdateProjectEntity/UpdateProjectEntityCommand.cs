using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Project.UpdateProjectEntity;

public record UpdateProjectEntityCommand(int Id, string Name, string Description, int StatusId, DateTime StartDate, DateTime? EndDate) : IRequest<int?>;