using AdvancedTaskManagementSystemCQRS.WebApi.Account;

namespace AdvancedTaskManagementSystemCQRS.WebApi.DependencyInjection;

public static class WebApiServiceCollectionExtensions
{
    public static IServiceCollection AddWebApiMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(AccountProfileWebApi).Assembly);
        });
        return services;
    }
}