using FlightApp.Application.Abstractions;
using FlightApp.Application.Flights.CreateFlight;
using FlightApp.Domain.Flights;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.UpdateFlight
{
    internal class UpdateFlightCommandHandler(IFlightRepository _flightRepository, IValidator<UpdateFlightCommand> _validator)
        : IRequestHandler<UpdateFlightCommand, Result>
    {
        public async Task<Result> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Failed(Error.ValidationErrorsToResultErrors(validationResult.Errors));
            }

            var flight = await _flightRepository.GetByIdAsync(request.Id);

            flight.Update(request.FlightNumber,
                request.FlightDate,
                request.Departure,
                request.Destination,
                request.AirplaneType
                );

            await _flightRepository.UpdateAsync(flight);

            return Result.Succeeded();
        }
    }
}
