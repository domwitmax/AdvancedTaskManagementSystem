using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanViewUserProjects;

public class CanViewUserProjectsQueryHandler : IRequestHandler<CanViewUserProjectsQuery, bool>
{
    public Task<bool> Handle(CanViewUserProjectsQuery request, CancellationToken cancellationToken)
    {
        var canView = request.IsAdministrator ||
            (!string.IsNullOrWhiteSpace(request.CurrentUserLogin) &&
             request.CurrentUserLogin.Equals(
                 request.RequestedUserLogin,
                 StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(canView);
    }
}
