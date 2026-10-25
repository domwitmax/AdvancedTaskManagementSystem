using AdvancedTaskManagementSystemCQRS.IntegrationTests.Infrastructure;
using System.Net;
using System.Net.Http.Json;

namespace AdvancedTaskManagementSystemCQRS.IntegrationTests.Api;

public class TaskApiIntegrationTests(CqrsWebApplicationFactory factory)
    : IClassFixture<CqrsWebApplicationFactory>
{
    [Fact]
    public async Task ProtectedEndpoint_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        using var client = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });

        var response = await client.GetAsync("/api/Task/GetListAllTasks");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetListAllTasks_AsAdministrator_ShouldReturnOk()
    {
        using var client = factory.CreateAuthenticatedClient();

        var response = await client.GetAsync("/api/Task/GetListAllTasks");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetTaskById_WhenTaskDoesNotExist_ShouldReturnNotFound()
    {
        using var client = factory.CreateAuthenticatedClient();

        var response = await client.GetAsync("/api/Task/GetTaskById/999999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task CreateTask_ShouldReturnCreatedAndAllowReadingCreatedTask()
    {
        using var client = factory.CreateAuthenticatedClient("ProjectManager");

        var projectResponse = await client.PostAsJsonAsync("/api/Project/CreateProject", new
        {
            Name = $"Projekt zadania {Guid.NewGuid():N}",
            Description = "Projekt do testu tworzenia zadania",
            StatusId = 1,
            StartDate = DateTime.UtcNow.Date,
            EndDate = (DateTime?)null
        });
        projectResponse.EnsureSuccessStatusCode();
        var projectId = await projectResponse.Content.ReadFromJsonAsync<int>();

        var taskResponse = await client.PostAsJsonAsync("/api/Task/CreateTask", new
        {
            Title = "Zadanie integracyjne",
            Description = "Zadanie utworzone w pełnym przepływie HTTP",
            TypeId = 1,
            StatusId = 1,
            ProjectId = projectId,
            PriorityId = 1,
            DueDate = DateTime.UtcNow.Date.AddDays(2),
            AssignedTo = "integration-user",
            LabelIds = Array.Empty<int>()
        });

        Assert.Equal(HttpStatusCode.Created, taskResponse.StatusCode);
        Assert.NotNull(taskResponse.Headers.Location);
        Assert.Contains("/api/Task/GetTaskById/", taskResponse.Headers.Location!.ToString());

        var getResponse = await client.GetAsync(taskResponse.Headers.Location);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

}
