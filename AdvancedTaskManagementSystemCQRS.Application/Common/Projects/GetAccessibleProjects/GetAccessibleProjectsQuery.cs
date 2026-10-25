using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetAccessibleProjects;

public record GetAccessibleProjectsQuery(
    string? UserLogin,
    bool IsAdministrator) : IRequest<List<ProjectDTO>>;
