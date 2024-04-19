using FlightApp.Application.Abstractions;
using FlightApp.Application.Flights.CreateFlight;
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

namespace FlightApp.Application.Flights.UpdateFlight
{
    internal class UpdateFlightCommandHandler(IFlightRepository _flightRepository, IValidator<UpdateFlightCommand> _validator,
        IAirplaneTypeRepository airplaneTypeRepository, IAirportRepository airportRepository)
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

            var departure = await airportRepository.GetByIataAsync(request.Departure.ToUpper());
            var destination = await airportRepository.GetByIataAsync(request.Destination.ToUpper());
            var airplaneType = await airplaneTypeRepository.GetByAirplaneNameAsync(request.AirplaneType.ToUpper());

            flight.Update(
                request.FlightNumber,
                request.FlightDate,
                departure!,
                destination!,
                airplaneType!
                );

            await _flightRepository.UpdateAsync(flight);

            return Result.Succeeded();
        }
    }
}
