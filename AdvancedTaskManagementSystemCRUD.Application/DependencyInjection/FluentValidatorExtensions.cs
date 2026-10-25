using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AdvancedTaskManagementSystemCRUD.Application.DependencyInjection
{
    public static class FluentValidatorExtensions
    {
        public static IServiceCollection AddFluentValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(FluentValidatorExtensions).Assembly);

            return services;
        }
    }
}
