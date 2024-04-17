using FlightApp.Application.Flights.CreateFlight;
using FlightApp.Application.Flights.DeleteFlight;
using FlightApp.Application.Flights.FindFlight;
using FlightApp.Application.Flights.UpdateFlight;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.ConfigureFluentValidator();
            services.ConfigureMediatR();

            return services;
        }
    }
}
