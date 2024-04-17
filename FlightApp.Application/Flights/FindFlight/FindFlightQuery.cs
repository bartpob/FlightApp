using FlightApp.Application.Abstractions;
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

    public sealed record FindFlightResponse(string FlightNumber, DateTime FlightDate,
            string Departure, string Destination, string AirplaneType);
}
