using FlightApp.Domain.Flights;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.DeleteFlight
{
    internal class DeleteFlightValidator
        : AbstractValidator<DeleteFlightCommand>
    {
        public DeleteFlightValidator(IFlightRepository flightRepository)
        {
            RuleFor(flight => flight.Id).MustAsync(async (id, Cancellation) =>
            {
                var flight = await flightRepository.GetByIdAsync(id);
                return flight != null;
            }).WithErrorCode("NOT_EXISTS")
              .WithMessage("Flight with given id doesn't exist");
        }
    }
}
