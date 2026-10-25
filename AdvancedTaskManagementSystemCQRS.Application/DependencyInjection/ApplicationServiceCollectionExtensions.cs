using AdvancedTaskManagementSystemCQRS.Application.Behaviors;
using AdvancedTaskManagementSystemCQRS.Application.Profiles;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AdvancedTaskManagementSystemCQRS.Application.DependencyInjection;

public static class ApplicationAddMediatorExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAddMediatorExtensions).Assembly));
        services.AddValidatorsFromAssembly(typeof(ApplicationAddMediatorExtensions).Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

    public static IServiceCollection AddApplicationMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(AccountProfileApplication).Assembly);
        });

        return services;
    }
}