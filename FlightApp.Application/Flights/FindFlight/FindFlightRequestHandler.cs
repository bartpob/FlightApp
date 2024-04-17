using FlightApp.Application.Abstractions;
using FlightApp.Application.Flights.DeleteFlight;
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
    internal class FindFlightRequestHandler(IFlightRepository _flightRepository, IValidator<FindFlightQuery> _validator)
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

            flights = flights.Where(f => 
                f.Departure == request.Departure &&
                f.Destination == request.Destination &&
                f.FlightDate == request.FlightDate
            ).ToList();

            var flightsResponse = flights.Select(f => new FindFlightResponse(
                f.FlightNumber,
                f.FlightDate,
                f.Departure,
                f.Destination,
                f.AirplaneType
                )).ToList();

            return Result<List<FindFlightResponse>>.Succeeded(flightsResponse);
        }
    }
}
