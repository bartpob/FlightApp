using FlightApp.Domain.Flights;
using FlightApp.Infrastructure.Persistence.Repositories;
using FlightApp.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using FluentValidation;
using FlightApp.Infrastructure.Identity.LoginUser;

namespace FlightApp.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigurePersistence();
            services.ConfigureIdentity();
            services.ConfigureAuthentication(configuration);

            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(assembly);
            });

            services.AddScoped<IValidator<LoginUserCommand>, LoginUserValidator>();

            return services;
        }
    }
}
