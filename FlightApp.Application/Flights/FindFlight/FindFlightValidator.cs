using FlightApp.Domain.AirplaneTypes;
using FlightApp.Domain.Airports;
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
        public FindFlightValidator(IAirportRepository airportRepository)
        {
            RuleFor(flight => flight.Destination).NotNull().NotEmpty();
            RuleFor(flight => flight.Departure).NotNull().NotEmpty();
            RuleFor(flight => flight.FlightDate).NotNull().NotEmpty();

            RuleFor(flight => flight.Departure).MustAsync(async (iata, Cancellation) =>
            {
                var airport = await airportRepository.GetByIataAsync(iata);
                return airport != null;
            }).WithErrorCode("NOT_EXISTS")
            .WithMessage("Given iata code doesn't exist");

            RuleFor(flight => flight.Destination).MustAsync(async (iata, Cancellation) =>
            {
                var airport = await airportRepository.GetByIataAsync(iata);
                return airport != null;
            }).WithErrorCode("NOT_EXISTS")
             .WithMessage("Given iata code doesn't exist");
        }
    }
}
