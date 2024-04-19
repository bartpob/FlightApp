using FlightApp.Application.Abstractions;
using FlightApp.Domain.Airports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.FindFlight
{
    public sealed record FindFlightQuery(string Departure, string Destination, DateTime FlightDate)
        : IRequest<Result<List<FindFlightResponse>>>;

    public sealed record FindFlightResponse(string FlightNumber, string FlightDate,
            AirportResponse Departure, AirportResponse Destination, string AirplaneType);
    public sealed record AirportResponse(string Iata, string City, string Country)
    {
        public static AirportResponse ToAirportResponse(Airport airport)
        {
            return new AirportResponse(airport.IATA, airport.City, airport.Country);
        }
    }
}
