using FlightApp.Application.Abstractions;
using FlightApp.Application.Flights.DeleteFlight;
using FlightApp.Domain.Airports;
using FlightApp.Domain.Flights;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.FindFlight
{
    internal class FindFlightQueryHandler(IFlightRepository _flightRepository, IValidator<FindFlightQuery> _validator, IAirportRepository airportRepository)
        : IRequestHandler<FindFlightQuery, Result<List<FindFlightResponse>>>
    {
        public async Task<Result<List<FindFlightResponse>>> Handle(FindFlightQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result<List<FindFlightResponse>>.Failed(Error.ValidationErrorsToResultErrors(validationResult.Errors));
            }

            var flights = await _flightRepository.GetAllAsync();

            var departure = await airportRepository.GetByIataAsync(request.Departure.ToUpper());
            var destination = await airportRepository.GetByIataAsync(request.Destination.ToUpper());

            flights = flights.Where(f => 
                f.Departure == departure &&
                f.Destination == destination &&
                f.FlightDate.Date == request.FlightDate.Date
            ).ToList();

            var flightsResponse = flights.Select(f => new FindFlightResponse(
                f.FlightNumber,
                f.FlightDate.ToString("dd/MM/yyyy"),
                AirportResponse.ToAirportResponse(f.Departure),
                AirportResponse.ToAirportResponse(f.Destination),
                f.AirplaneType.Airplane
                )).ToList();

            return Result<List<FindFlightResponse>>.Succeeded(flightsResponse);
        }
    }
}
