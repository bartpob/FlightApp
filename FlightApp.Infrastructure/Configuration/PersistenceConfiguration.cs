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

        private static void AddDictionaryData(FlightAppDbContext dbContext)
        {
            dbContext.Airports.Add(Airport.Create("WAW", "Warsaw", "Poland"));
            dbContext.Airports.Add(Airport.Create("VIE", "Vienna", "Austria"));
            dbContext.Airports.Add(Airport.Create("VGO", "Vigo", "Spain"));
            dbContext.Airports.Add(Airport.Create("VNO", "Vilnius", "Lithuania"));
            dbContext.Airports.Add(Airport.Create("WAS", "Washington", "USA"));
            dbContext.Airports.Add(Airport.Create("WRO", "Wrocław", "Poland"));


            dbContext.AirplaneTypes.Add(AirplaneType.Create("DREAMLINER"));
            dbContext.AirplaneTypes.Add(AirplaneType.Create("AIRBUS"));
            dbContext.AirplaneTypes.Add(AirplaneType.Create("BOEING"));
        }
    }
}
