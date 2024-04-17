using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.CreateFlight
{
    internal class CreateFlightValidator
        : AbstractValidator<CreateFlightCommand>
    {
        public CreateFlightValidator()
        {
            RuleFor(flight => flight.FlightNumber).NotNull().NotEmpty();
            RuleFor(flight => flight.Destination).NotNull().NotEmpty();
            RuleFor(flight => flight.Departure).NotNull().NotEmpty();
            RuleFor(flight => flight.FlightDate).NotNull().NotEmpty();
            RuleFor(flight => flight.AirplaneType).NotNull().NotEmpty();


            RuleFor(flight => flight.Departure).NotEqual(flight => flight.Destination);
            RuleFor(flight => flight.Destination).NotEqual(flight => flight.Departure);

            RuleFor(flight => flight.FlightDate).GreaterThan(DateTime.UtcNow);
        }
    }
}
