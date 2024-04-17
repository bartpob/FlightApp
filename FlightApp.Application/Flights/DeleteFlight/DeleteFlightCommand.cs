using FlightApp.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.DeleteFlight
{
    public sealed record DeleteFlightCommand(Guid Id)
        : IRequest<Result>;
}
