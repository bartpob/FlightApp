using FlightApp.Application.Abstractions;
using FlightApp.Application.Flights.FindFlight;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.GetAllFlights
{
    public sealed record GetAllFlightsQuery
        : IRequest<Result<List<GetAllFlightsQueryReponse>>>;

    public sealed record GetAllFlightsQueryReponse(Guid Id, string FlightNumber, string FlightDate,
           AirportResponse Departure, AirportResponse Destination, string AirplaneType);
}
