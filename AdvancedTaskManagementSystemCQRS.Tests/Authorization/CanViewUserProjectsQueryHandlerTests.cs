using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanViewUserProjects;

namespace AdvancedTaskManagementSystemCQRS.Tests.Authorization;

public class CanViewUserProjectsQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenAdministrator_ShouldAllowDifferentUser()
    {
        var handler = new CanViewUserProjectsQueryHandler();

        var result = await handler.Handle(
            new CanViewUserProjectsQuery("admin", "student", true),
            CancellationToken.None);

        Assert.True(result);
    }

    [Theory]
    [InlineData("student", "student", true)]
    [InlineData("Student", "student", true)]
    [InlineData("student", "other", false)]
    [InlineData(null, "student", false)]
    public async Task Handle_ShouldApplyUserOwnershipPolicy(
        string? currentUser,
        string requestedUser,
        bool expected)
    {
        var handler = new CanViewUserProjectsQueryHandler();

        var result = await handler.Handle(
            new CanViewUserProjectsQuery(currentUser, requestedUser, false),
            CancellationToken.None);

        Assert.Equal(expected, result);
    }
}
