using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanViewUserProjects;

public record CanViewUserProjectsQuery(
    string? CurrentUserLogin,
    string RequestedUserLogin,
    bool IsAdministrator) : IRequest<bool>;
