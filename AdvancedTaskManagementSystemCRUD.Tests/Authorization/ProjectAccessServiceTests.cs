using AdvancedTaskManagementSystemCRUD.Application.Services.Authorization;
using AdvancedTaskManagementSystemCRUD.Application.Services.Project;
using AdvancedTaskManagementSystemCRUD.Application.Services.Project.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;
using Moq;

namespace AdvancedTaskManagementSystemCRUD.Tests.Authorization;

public class ProjectAccessServiceTests
{
    [Fact]
    public void CanAccessProject_WhenAdministrator_ShouldAllowWithoutMembershipCheck()
    {
        var projectService = new Mock<IProjectService>();
        var service = new ProjectAccessService(projectService.Object);

        var result = service.CanAccessProject(10, null, true);

        Assert.True(result);
        projectService.Verify(item => item.CanAccessProject(
            It.IsAny<int>(), It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void CanAccessProject_ShouldReturnMembershipResult(bool membership)
    {
        var projectService = new Mock<IProjectService>();
        projectService
            .Setup(item => item.CanAccessProject(10, "student"))
            .Returns(membership);
        var service = new ProjectAccessService(projectService.Object);

        var result = service.CanAccessProject(10, "student", false);

        Assert.Equal(membership, result);
    }

    [Fact]
    public void FilterAccessibleTasks_WhenAdministrator_ShouldReturnAllTasks()
    {
        var projectService = new Mock<IProjectService>();
        var service = new ProjectAccessService(projectService.Object);
        var tasks = CreateTasks();

        var result = service.FilterAccessibleTasks(tasks, null, true);

        Assert.Equal(2, result.Count);
        projectService.Verify(item => item.GetProjectsByUser(
            It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void FilterAccessibleTasks_ShouldReturnOnlyTasksFromUsersProjects()
    {
        var projectService = new Mock<IProjectService>();
        projectService
            .Setup(item => item.GetProjectsByUser("student"))
            .Returns(new List<ProjectDTO> { new() { Id = 2, Name = "Projekt 2" } });
        var service = new ProjectAccessService(projectService.Object);

        var result = service.FilterAccessibleTasks(CreateTasks(), "student", false);

        var task = Assert.Single(result);
        Assert.Equal(2, task.ProjectId);
    }

    [Theory]
    [InlineData("student", "student", false, true)]
    [InlineData("Student", "student", false, true)]
    [InlineData("student", "other", false, false)]
    [InlineData(null, "student", false, false)]
    [InlineData("admin", "student", true, true)]
    public void CanViewUserProjects_ShouldApplyPolicy(
        string? currentUser,
        string requestedUser,
        bool isAdministrator,
        bool expected)
    {
        var service = new ProjectAccessService(new Mock<IProjectService>().Object);

        var result = service.CanViewUserProjects(
            currentUser,
            requestedUser,
            isAdministrator);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetAccessibleProjects_WhenAdministrator_ShouldUseAllProjects()
    {
        var projectService = new Mock<IProjectService>();
        projectService
            .Setup(item => item.GetAllProjects())
            .Returns(new List<ProjectDTO> { new() { Id = 1, Name = "A" } });
        var service = new ProjectAccessService(projectService.Object);

        var result = service.GetAccessibleProjects("admin", true);

        Assert.Single(result);
        projectService.Verify(item => item.GetProjectsByUser(
            It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [InlineData("author", "author", false, true)]
    [InlineData("Author", "author", false, true)]
    [InlineData("author", "other", false, false)]
    [InlineData("author", null, false, false)]
    [InlineData("author", "admin", true, true)]
    public void CanModifyComment_ShouldApplyOwnershipPolicy(
        string createdBy,
        string? currentUser,
        bool isAdministrator,
        bool expected)
    {
        var service = new ProjectAccessService(new Mock<IProjectService>().Object);

        var result = service.CanModifyComment(
            createdBy,
            currentUser,
            isAdministrator);

        Assert.Equal(expected, result);
    }

    private static List<TaskDTO> CreateTasks() =>
    [
        new TaskDTO { Id = 1, ProjectId = 1, Title = "Zadanie 1" },
        new TaskDTO { Id = 2, ProjectId = 2, Title = "Zadanie 2" }
    ];
}
