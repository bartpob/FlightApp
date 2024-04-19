using FlightApp.Application.Abstractions;
using FlightApp.Domain.AirplaneTypes;
using FlightApp.Domain.Airports;
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
    internal class CreateFlightCommandHandler(IFlightRepository _flightRepository, IValidator<CreateFlightCommand> _validator,
        IAirplaneTypeRepository airplaneTypeRepository, IAirportRepository airportRepository)
        : IRequestHandler<CreateFlightCommand, Result>
    {
        public async Task<Result> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Failed(Error.ValidationErrorsToResultErrors(validationResult.Errors));
            }

            var departure = await airportRepository.GetByIataAsync(request.Departure.ToUpper());
            var destination = await airportRepository.GetByIataAsync(request.Destination.ToUpper());
            var airplaneType = await airplaneTypeRepository.GetByAirplaneNameAsync(request.AirplaneType.ToUpper());

            await _flightRepository.CreateAsync(Flight.Create(
                request.FlightNumber,
                request.FlightDate,
                departure!,
                destination!,
                airplaneType!
                ));

            return Result.Succeeded();
        }
    }
}
