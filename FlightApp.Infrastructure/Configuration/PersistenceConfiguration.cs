using FlightApp.Domain.AirplaneTypes;
using FlightApp.Domain.Airports;
using FlightApp.Domain.Flights;
using FlightApp.Infrastructure.Persistence;
using FlightApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.Configuration
{
    public static class PersistenceConfiguration
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services)
        {
            services.AddDbContext<FlightAppDbContext>(options => options.UseInMemoryDatabase(databaseName: "FlightAppDb"));

            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IAirplaneTypeRepository, AirplaneTypeRepository>();
            services.AddScoped<IAirportRepository, AirportRepository>();

            return services;
        }
    }
}
