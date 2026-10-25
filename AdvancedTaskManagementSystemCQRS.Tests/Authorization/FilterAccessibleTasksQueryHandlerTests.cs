using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectsByUser;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.FilterAccessibleTasks;
using MediatR;
using Moq;

namespace AdvancedTaskManagementSystemCQRS.Tests.Authorization;

public class FilterAccessibleTasksQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenAdministrator_ShouldReturnAllTasksWithoutProjectLookup()
    {
        var mediator = new Mock<IMediator>();
        var tasks = CreateTasks();
        var handler = new FilterAccessibleTasksQueryHandler(mediator.Object);

        var result = await handler.Handle(
            new FilterAccessibleTasksQuery(tasks, null, true),
            CancellationToken.None);

        Assert.Equal(2, result.Count);
        mediator.Verify(item => item.Send(
            It.IsAny<GetProjectsByUserQuery>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenUserLoginIsMissing_ShouldReturnEmptyList()
    {
        var handler = new FilterAccessibleTasksQueryHandler(new Mock<IMediator>().Object);

        var result = await handler.Handle(
            new FilterAccessibleTasksQuery(CreateTasks(), null, false),
            CancellationToken.None);

        Assert.Empty(result);
    }

    [Fact]
    public async Task Handle_ShouldReturnOnlyTasksFromUsersProjects()
    {
        var mediator = new Mock<IMediator>();
        mediator
            .Setup(item => item.Send(
                It.Is<GetProjectsByUserQuery>(query => query.UserName == "student"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ProjectDTO>
            {
                new() { Id = 2, Name = "Projekt 2" }
            });

        var handler = new FilterAccessibleTasksQueryHandler(mediator.Object);

        var result = await handler.Handle(
            new FilterAccessibleTasksQuery(CreateTasks(), "student", false),
            CancellationToken.None);

        var task = Assert.Single(result);
        Assert.Equal(2, task.ProjectId);
    }

    private static List<GetTaskDTO> CreateTasks() =>
    [
        new GetTaskDTO { Id = 1, ProjectId = 1, Title = "Zadanie 1" },
        new GetTaskDTO { Id = 2, ProjectId = 2, Title = "Zadanie 2" }
    ];
}
