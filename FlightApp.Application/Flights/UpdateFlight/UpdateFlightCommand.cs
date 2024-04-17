using FlightApp.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.UpdateFlight
{
    public sealed record UpdateFlightCommand(Guid Id, string FlightNumber, DateTime FlightDate,
        string Departure, string Destination, string AirplaneType)
        : IRequest<Result>;

    public sealed record UpdateFlightCommandRequest(string FlightNumber, DateTime FlightDate,
        string Departure, string Destination, string AirplaneType);
}
