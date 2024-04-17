using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.FindFlight
{
    internal class FindFlightValidator
        : AbstractValidator<FindFlightQuery>
    {
        public FindFlightValidator()
        {
            RuleFor(flight => flight.Destination).NotNull().NotEmpty();
            RuleFor(flight => flight.Departure).NotNull().NotEmpty();
            RuleFor(flight => flight.FlightDate).NotNull().NotEmpty();
        }
    }
}
