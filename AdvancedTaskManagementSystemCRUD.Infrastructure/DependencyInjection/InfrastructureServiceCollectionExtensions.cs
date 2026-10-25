using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Services.PasswordHasher;
using AdvancedTaskManagementSystemCRUD.Application.Services.Token;
using AdvancedTaskManagementSystemCRUD.Infrastructure.Repository;
using AdvancedTaskManagementSystemCRUD.Infrastructure.Services.PasswordHasher;
using AdvancedTaskManagementSystemCRUD.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IChangesHistoryRepository, ChangesHistoryRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHasher, PasswordHasherService>();
        return services;
    }
}
