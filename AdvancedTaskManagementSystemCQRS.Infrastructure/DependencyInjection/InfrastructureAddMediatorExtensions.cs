using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.DependencyInjection
{
    public static class InfrastructureAddMediatorExtensions
    {
        public static IServiceCollection AddInfrastructureMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(InfrastructureAddMediatorExtensions).Assembly));
            return services;
        }
    }
}
