using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AdvancedTaskManagementSystemCQRS.Infrastructure;
using AdvancedTaskManagementSystemCQRS.WebApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace AdvancedTaskManagementSystemCQRS.IntegrationTests.Infrastructure;

public sealed class CqrsWebApplicationFactory : WebApplicationFactory<WebApiEntryPoint>
{
    private readonly InMemoryDatabaseRoot _databaseRoot = new();
    private readonly string _databaseName = $"CQRS-Tests-{Guid.NewGuid():N}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<TaskManagerDbContext>();
            services.RemoveAll<DbContextOptions<TaskManagerDbContext>>();
            services.RemoveAll<DbContextOptions>();

            var existingOptionConfigurations = services
                .Where(descriptor =>
                    descriptor.ServiceType.IsGenericType &&
                    descriptor.ServiceType.GenericTypeArguments.Contains(typeof(TaskManagerDbContext)) &&
                    descriptor.ServiceType.Name.StartsWith("IDbContextOptionsConfiguration", StringComparison.Ordinal))
                .ToList();

            foreach (var descriptor in existingOptionConfigurations)
                services.Remove(descriptor);

            services.AddDbContext<TaskManagerDbContext>(options =>
                options.UseInMemoryDatabase(_databaseName, _databaseRoot));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = TestAuthHandler.Scheme;
                options.DefaultChallengeScheme = TestAuthHandler.Scheme;
                options.DefaultScheme = TestAuthHandler.Scheme;
            }).AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                TestAuthHandler.Scheme,
                _ => { });

        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        using var scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagerDbContext>();
        dbContext.Database.EnsureCreated();

        if (!dbContext.Account.Any(account => account.Login == "integration-user"))
        {
            dbContext.Account.Add(new AccountEntity
            {
                Login = "integration-user",
                Email = "integration-user@example.test",
                PasswordHash = "test-hash",
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastGetUserAt = DateTime.UtcNow,
                Active = true,
                RoleId = 2
            });
            dbContext.SaveChanges();
        }

        return host;
    }

    public HttpClient CreateAuthenticatedClient(string role = "Administrator", string user = "integration-user")
    {
        var client = CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });
        client.DefaultRequestHeaders.Add("X-Test-User", user);
        client.DefaultRequestHeaders.Add("X-Test-Role", role);
        return client;
    }
}
