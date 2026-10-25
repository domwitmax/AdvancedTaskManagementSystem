using AdvancedTaskManagementSystemCRUD.IntegrationTests.Infrastructure;
using System.Net;
using System.Net.Http.Json;

namespace AdvancedTaskManagementSystemCRUD.IntegrationTests.Api;

public class ProjectApiIntegrationTests(CrudWebApplicationFactory factory)
    : IClassFixture<CrudWebApplicationFactory>
{
    [Fact]
    public async Task CreateProject_ShouldReturnCreatedWithLocationPointingToGetProjectById()
    {
        using var client = factory.CreateAuthenticatedClient("ProjectManager");
        var request = new
        {
            Name = $"Projekt integracyjny {Guid.NewGuid():N}",
            Description = "Projekt utworzony przez test integracyjny",
            StatusId = 1,
            StartDate = DateTime.UtcNow.Date,
            EndDate = (DateTime?)null
        };

        var response = await client.PostAsJsonAsync("/api/Project/CreateProject", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        Assert.Contains("/api/Project/GetProjectById/", response.Headers.Location!.ToString());

        var getResponse = await client.GetAsync(response.Headers.Location);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    [Fact]
    public async Task GetProjectById_WhenUserIsNotProjectMember_ShouldReturnForbidden()
    {
        using var managerClient = factory.CreateAuthenticatedClient("ProjectManager");
        var createResponse = await managerClient.PostAsJsonAsync("/api/Project/CreateProject", new
        {
            Name = $"Projekt autoryzacji {Guid.NewGuid():N}",
            Description = "Projekt do testu uprawnień",
            StatusId = 1,
            StartDate = DateTime.UtcNow.Date,
            EndDate = (DateTime?)null
        });
        createResponse.EnsureSuccessStatusCode();
        var projectId = await createResponse.Content.ReadFromJsonAsync<int>();

        using var otherUserClient = factory.CreateAuthenticatedClient("TeamMember", "other-user");
        var response = await otherUserClient.GetAsync($"/api/Project/GetProjectById/{projectId}");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}
