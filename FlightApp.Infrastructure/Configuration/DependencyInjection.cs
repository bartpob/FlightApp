using FlightApp.Domain.Flights;
using FlightApp.Infrastructure.Persistence.Repositories;
using FlightApp.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.ConfigurePersistence();

            return services;
        }
    }
}
