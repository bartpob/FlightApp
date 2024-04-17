using FlightApp.Application.Abstractions;
using FlightApp.Domain.Flights;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.CreateFlight
{
    internal class CreateFlightCommandHandler(IFlightRepository _flightRepository, IValidator<CreateFlightCommand> _validator)
        : IRequestHandler<CreateFlightCommand, Result>
    {
        public async Task<Result> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Failed(Error.ValidationErrorsToResultErrors(validationResult.Errors));
            }

            await _flightRepository.CreateAsync(Flight.Create(
                request.FlightNumber,
                request.FlightDate,
                request.Departure,
                request.Destination,
                request.AirplaneType
                ));

            return Result.Succeeded();
        }
    }
}
