using FlightApp.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.Identity.LoginUser
{
    public sealed record LoginUserCommand(string Email, string Password)
        : IRequest<Result<LoginUserResponse>>;

    public sealed record LoginUserResponse(string Email, string AuthToken);
}
