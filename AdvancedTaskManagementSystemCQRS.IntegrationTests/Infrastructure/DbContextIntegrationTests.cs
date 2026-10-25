using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AdvancedTaskManagementSystemCQRS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.IntegrationTests.Infrastructure;

public class DbContextIntegrationTests
{
    [Fact]
    public async Task DbContext_ShouldPersistAndLoadProjectWithStatus()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseInMemoryDatabase($"CQRS-DbContext-{Guid.NewGuid():N}")
            .Options;

        await using var dbContext = new TaskManagerDbContext(options);
        await dbContext.Database.EnsureCreatedAsync();

        var project = new ProjectEntity
        {
            Name = "Projekt persystencji",
            Description = "Test modelu EF Core",
            StatusId = 1,
            StartDate = DateTime.UtcNow.Date
        };
        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();

        var loaded = await dbContext.Projects
            .Include(item => item.Status)
            .SingleAsync(item => item.Id == project.Id);

        Assert.Equal("Projekt persystencji", loaded.Name);
        Assert.Equal("Nowy", loaded.Status.Name);
    }
}
