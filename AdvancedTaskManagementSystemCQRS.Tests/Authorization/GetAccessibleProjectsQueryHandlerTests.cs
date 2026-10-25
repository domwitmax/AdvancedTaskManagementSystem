using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetAccessibleProjects;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetAllProjects;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectsByUser;
using MediatR;
using Moq;

namespace AdvancedTaskManagementSystemCQRS.Tests.Authorization;

public class GetAccessibleProjectsQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenAdministrator_ShouldUseAllProjectsQuery()
    {
        var mediator = new Mock<IMediator>();
        mediator
            .Setup(item => item.Send(
                It.IsAny<GetAllProjectsQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ProjectDTO> { new() { Id = 1, Name = "A" } });

        var handler = new GetAccessibleProjectsQueryHandler(mediator.Object);
        var result = await handler.Handle(
            new GetAccessibleProjectsQuery("admin", true),
            CancellationToken.None);

        Assert.Single(result);
        mediator.Verify(item => item.Send(
            It.IsAny<GetProjectsByUserQuery>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenRegularUser_ShouldUseProjectsByUserQuery()
    {
        var mediator = new Mock<IMediator>();
        mediator
            .Setup(item => item.Send(
                It.Is<GetProjectsByUserQuery>(query => query.UserName == "student"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ProjectDTO> { new() { Id = 2, Name = "B" } });

        var handler = new GetAccessibleProjectsQueryHandler(mediator.Object);
        var result = await handler.Handle(
            new GetAccessibleProjectsQuery("student", false),
            CancellationToken.None);

        var project = Assert.Single(result);
        Assert.Equal(2, project.Id);
    }
}
