using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.DependencyInjection;

public static class InfrastructureAddTaskManagerDbExtensions
{
    public static void AddTaskManagerDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskManagerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("TaskManagerCRUD")));
    }
}
