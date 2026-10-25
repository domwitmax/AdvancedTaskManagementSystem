using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanAccessProject;

public record CanAccessProjectQuery(
    int ProjectId,
    string? UserLogin,
    bool IsAdministrator = false) : IRequest<bool>;
