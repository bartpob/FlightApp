using FlightApp.Application.Abstractions;
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

    public sealed record GetAllFlightsQueryReponse(Guid Id, string FlightNumber, DateTime FlightDate,
           string Departure, string Destination, string AirplaneType);
}
