using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Configuration
{
    public static class MediatRConfiguration
    {
        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            var assembly = typeof(MediatRConfiguration).Assembly;

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(assembly);
            });

            return services;
        }
    }
}
