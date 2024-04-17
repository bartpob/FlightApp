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
    public static class FluentValidatorConfiguration
    {
        public static IServiceCollection ConfigureFluentValidator(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateFlightCommand>, CreateFlightValidator>();
            services.AddScoped<IValidator<DeleteFlightCommand>, DeleteFlightValidator>();
            services.AddScoped<IValidator<FindFlightQuery>, FindFlightValidator>();
            services.AddScoped<IValidator<UpdateFlightCommand>, UpdateFlightValidator>();

            return services;
        }
    }
}
