using FlightApp.Application.Flights.CreateFlight;
using FlightApp.Domain.Flights;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.UpdateFlight
{
    internal class UpdateFlightValidator
         : AbstractValidator<UpdateFlightCommand>
    {
        public UpdateFlightValidator(IFlightRepository flightRepository)
        {
            RuleFor(flight => flight.Id).MustAsync(async (id, Cancellation) =>
            {
                var flight = await flightRepository.GetByIdAsync(id);
                return flight != null;
            }).WithErrorCode("NOT_EXISTS")
              .WithMessage("Flight with given id doesn't exist");


            RuleFor(flight => flight.FlightNumber).NotNull().NotEmpty();
            RuleFor(flight => flight.Destination).NotNull().NotEmpty();
            RuleFor(flight => flight.Departure).NotNull().NotEmpty();
            RuleFor(flight => flight.FlightDate).NotNull().NotEmpty();
            RuleFor(flight => flight.AirplaneType).NotNull().NotEmpty();


            RuleFor(flight => flight.Departure).NotEqual(flight => flight.Destination);
            RuleFor(flight => flight.Destination).NotEqual(flight => flight.Departure);
        }
    }
}
