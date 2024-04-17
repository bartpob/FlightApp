using FlightApp.Application.Abstractions;
using FlightApp.Application.Flights.CreateFlight;
using FlightApp.Domain.Flights;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Flights.DeleteFlight
{
    internal class DeleteFlightCommandHandler(IFlightRepository _flightRepository, IValidator<DeleteFlightCommand> _validator)
        : IRequestHandler<DeleteFlightCommand, Result>
    {
        public async Task<Result> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result.Failed(Error.ValidationErrorsToResultErrors(validationResult.Errors));
            }

            await _flightRepository.DeleteAsync(request.Id);

            return Result.Succeeded();
        }
    }
}
