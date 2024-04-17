using FlightApp.Application.Abstractions;
using FlightApp.Application.Flights.FindFlight;
using FlightApp.Domain.Flights;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.GetAllFlights
{
    internal class GetAllFlightsQueryHandler(IFlightRepository _flightRepository)
        : IRequestHandler<GetAllFlightsQuery, Result<List<GetAllFlightsQueryReponse>>>
    {
        public async Task<Result<List<GetAllFlightsQueryReponse>>> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
        {
            var flights = await _flightRepository.GetAllAsync();

            var flightsResponse = flights.Select(f => new GetAllFlightsQueryReponse(
                 f.Id,
                 f.FlightNumber,
                 f.FlightDate,
                 f.Departure,
                 f.Destination,
                 f.AirplaneType
                 )).ToList();

            return Result<List<GetAllFlightsQueryReponse>>.Succeeded(flightsResponse);
        }
    }
}
