using FlightApp.Application.Abstractions;
using FlightApp.Application.Flights.CreateFlight;
using FlightApp.Application.Flights.DeleteFlight;
using FlightApp.Application.Flights.FindFlight;
using FlightApp.Application.Flights.GetAllFlights;
using FlightApp.Application.Flights.UpdateFlight;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController(IMediator _mediator) 
        : ControllerBase
    {
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult> CreateFlight(CreateFlightCommand command)
        {
            var result = await _mediator.Send(command);

            if(result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("id")]
        public async Task<ActionResult> UpdateFlight(Guid id, UpdateFlightCommandRequest request)
        {
            var command = new UpdateFlightCommand(id, request.FlightNumber, request.FlightDate, 
                         request.Departure, request.Destination, request.AirplaneType);

            var result = await _mediator.Send(command);

            if(result.IsFailed)
            {
                foreach(var error in result.Errors)
                {
                    if(error.Code == "NOT_EXISTS")
                    {
                        return NotFound(error);
                    }
                }

                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteFlight(Guid id)
        {
            var command = new DeleteFlightCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return NotFound(result.Errors);
            }

            return Ok();

        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<ActionResult<List<FindFlightResponse>>> GetAllFlights()
        {
            var query = new GetAllFlightsQuery();

            var result = await _mediator.Send(query);

            return Ok(result.Body);
        }

        [HttpGet]
        [Route("find")]
        public async Task<ActionResult<List<FindFlightResponse>>> FindFlight([FromQuery] FindFlightQuery query)
        {
            var result = await _mediator.Send(query);
            if(result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Body);
        }


    }
}
