using FlightApp.Infrastructure.Identity.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator _mediator)
        : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginUserResponse>> Login(LoginUserCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand);

            if (result.IsFailed)
            {
                return Unauthorized(result.Errors);
            }

            return Accepted(result.Body);
        }
    }
}
