using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanAccessProject;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.IsProjectMember;
using MediatR;
using Moq;

namespace AdvancedTaskManagementSystemCQRS.Tests.Authorization;

public class CanAccessProjectQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenAdministrator_ShouldAllowWithoutRepositoryLookup()
    {
        var mediator = new Mock<IMediator>();
        var handler = new CanAccessProjectQueryHandler(mediator.Object);

        var result = await handler.Handle(
            new CanAccessProjectQuery(10, null, true),
            CancellationToken.None);

        Assert.True(result);
        mediator.Verify(item => item.Send(
            It.IsAny<IsProjectMemberQuery>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenUserLoginIsMissing_ShouldDeny()
    {
        var mediator = new Mock<IMediator>();
        var handler = new CanAccessProjectQueryHandler(mediator.Object);

        var result = await handler.Handle(
            new CanAccessProjectQuery(10, null, false),
            CancellationToken.None);

        Assert.False(result);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Handle_ShouldReturnMembershipResult(bool isMember)
    {
        var mediator = new Mock<IMediator>();
        mediator
            .Setup(item => item.Send(
                It.Is<IsProjectMemberQuery>(query =>
                    query.ProjectId == 10 && query.UserLogin == "student"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(isMember);

        var handler = new CanAccessProjectQueryHandler(mediator.Object);

        var result = await handler.Handle(
            new CanAccessProjectQuery(10, "student", false),
            CancellationToken.None);

        Assert.Equal(isMember, result);
    }
}
