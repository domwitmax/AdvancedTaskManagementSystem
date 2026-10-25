using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectById;

public record GetProjectByIdQuery(int ProjectId) : IRequest<ProjectDTO?>;
